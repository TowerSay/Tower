using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct NodeConnect
{
	public enum TYPE
	{
		LEFT,
		RIGHT,
		UP,
		DOWN
	}

	public Point id;
	public TYPE type;

	public NodeConnect(Point id,TYPE type)
	{
		this.id=id;
		this.type=type;
	}

	public NodeConnect(int x,int y,TYPE type)
	{
		this.id=new Point(x,y);
		this.type=type;
	}
}

public class MapSRTM : SRTMBase 
{
	Transform _nodeRoot;
	List<RoomNodeCtl> rnc_lst=new List<RoomNodeCtl>();

	Dictionary<Point,RoomNodeCtl> rnc_dic=new Dictionary<Point, RoomNodeCtl>();
	RoomNodeCtl[,] rnc_ary;

	List<Point> hSid=new List<Point>();	//已经查找到的ids
	List<Point> lSid=new List<Point>();	//剩余的ids
	Point mapSize;
	Point pre,aim;

	public Transform nodeRoot
	{
		get
		{
			if(_nodeRoot==null)_nodeRoot=GameObject.Find("NodeRoot").transform;
			return _nodeRoot;
		}
	}

	void Start () 
	{
		OnStart();
		/*
		for(int i=0;i<5;i++)
		{
			for(int o=0;o<5;o++)
			{
				GameObject go=GameObject.Instantiate(Game.SObj("Node")) as GameObject;
				go.transform.parent=nodeRoot;
				go.transform.localPosition=new Vector3(i*180,o*180,0);
				go.transform.localScale=Vector3.one;
				RoomNodeCtl rnc=go.GetComponent<RoomNodeCtl>();
				ButtonMsgObj bmo=go.GetComponent<ButtonMsgObj>();
				bmo.value=rnc;
				bmo.target=this.gameObject;

				rnc_lst.Add(rnc);
			}
		}*/
	}

	void NodeChose(RoomNodeCtl rnc)
	{
		rnc.right=!rnc.right;
		rnc.down=!rnc.down;
	}

	void ClearMap()
	{

		for(int i=0;i<rnc_lst.Count;i++)
		{
			Destroy(rnc_lst[i].gameObject);
		}

		rnc_lst.Clear();
		rnc_dic.Clear();

		hSid.Clear();
		lSid.Clear();
	}



	//分布房间类型
	void DistributionRoomType()
	{
		int depth=0;
		int max=0;
		
		//预留最后一组
		List<Point>last_hfs=new List<Point>();
		
		//已寻找列表
		List<Point>lfs=new List<Point>();
		List<int>lfs_z=new List<int>();
		lfs.Add(pre);
		
		//欲寻找列表
		List<Point>hfs=GetConnect(pre);
		
		while(hfs.Count>0)
		{
			depth++;
			
			//下个循环 欲查找列表
			List<Point>next_hfs=new List<Point>();
			last_hfs.Clear();
			for(int i=0;i<hfs.Count;i++)
			{
				Point v=hfs[i];
				//四向连结点列表
				List<Point>lst=GetConnect(v);
				
				foreach(Point lsv in lst)
				{
					//若已找到的列表未包含该点 ，添加到预搜索列表
					if(!lfs.Contains(lsv))
					{
						next_hfs.Add(lsv);
					}
				}

				RoomNodeCtl rnc=rnc_ary[v.x,v.y];
				if(depth>rnc.depth)
				{

					rnc.typeLabel.text=depth.ToString();
					rnc.depth=depth;
					rnc.spt.color=Color.Lerp(new Color(0f,0.85f,1f),Color.yellow,(float)depth/20f);
				}

				/*
				SSCC_SP cvsp=scs[(int)v.x,(int)v.y];
				cvsp.transform.GetComponent<SpriteRenderer>().color=new Color(1,1,0,1);
				cvsp.transform.parent.Find("ff").GetComponent<UILabel>().text=depth.ToString();
				*/
			}
			
			foreach(Point lsv in hfs){if(!lfs.Contains(lsv)){lfs.Add(lsv);lfs_z.Add(depth);last_hfs.Add(lsv);}}//	last_hfs.Add(lsv);}
			
			hfs=next_hfs;
		}

		Debug.Log("始末最大距离："+depth);

		//选中维度最大的房间作为下一层入口
		aim=last_hfs[Random.Range(0,last_hfs.Count)];
		lfs.Remove(aim);
		lfs.Remove(pre);

		rnc_ary[aim.x,aim.y].typeLabel.text="下层";
		rnc_ary[pre.x,pre.y].typeLabel.text="起点";

		rnc_ary[aim.x,aim.y].spt.color=Color.yellow;
		rnc_ary[pre.x,pre.y].spt.color=Color.white;

		/*
		//分布房间类型
		for(int i=0;i<lfs.size;i++)
		{
			//深度指数 0~1
			float r=(float)lfs_z[i]/(float)depth;
			
			SSCC_SP cvsp=scs[(int)lfs[i].x,(int)lfs[i].y];
			//cvsp.transform.GetComponent<SpriteRenderer>().color=new Color(1,1,0,1);
			int type=Random.Range(1,51);
			string str="";
			
			if(type>20)
			{
				//波动值
				float crl=r*0.25f;
				r+=Random.Range(-crl,crl);
				
				if(r>=1)str="[ff5000]BOSS!!!";
				else str="Lv:"+(r*10).ToString("F1");
			}
			else if(type>15)
			{
				str="[00eeff]NPC";
			}
			else if(type>10)
			{
				str="[00ffff]陷阱";
			}
			else if(type>5)
			{
				str="[00ff00]商人";
			}
			else if(type>0)
			{
				str="[ffef00]宝物";
			}
			
			cvsp.transform.parent.Find("ff").GetComponent<UILabel>().text=str;
			cvsp.transform.parent.Find("ff").localScale=Vector3.one*0.56f;
			//Debug.Log(r.ToString());
			
		}*/
	}

	//更新通道连接线
	void CreateLine(bool r,bool d,RoomNodeCtl rnc)
	{
		if(r)
		{
			rnc.right=r;
		}
		if(d)
		{
			rnc.down=r;
		}
	}

	void OnStart()
	{
		//清除地图
		ClearMap();

		//重定义地图尺寸
		//mapSize=new Point(GameHelp.Random(5,10),GameHelp.Random(5,10));
		mapSize=new Point(10,10);

		//选择起始点
		pre=new Point(GameHelp.Random(0,mapSize.x),GameHelp.Random(0,mapSize.y));

		//初始化地图寄存数组
		rnc_ary=new RoomNodeCtl[mapSize.x,mapSize.y];

		//地图中心偏移
		float mdx=(mapSize.x*0.5f);
		float mdy=(mapSize.y*0.5f);

		//最右边和最下边 索引
		int l_x=mapSize.x-1;
		int l_y=mapSize.y-1;

		//房间图块尺寸
		float size=180f;
		float halfSize=size*0.5f;
	
		//先创建基本房间矩阵
		for(int i=0;i<mapSize.x;i++)
		{
			bool far=false;
			bool fad=false;
			
			for(int o=0;o<mapSize.y;o++)
			{
				int e_x=i-1;
				int e_y=o-1;

				//创建房间物体
				GameObject go=GameObject.Instantiate(Game.SObj("Node")) as GameObject;
				go.transform.parent=nodeRoot;
				go.transform.localPosition=new Vector3((i-mdx)*size+halfSize,(mdy-o)*size-halfSize,0);
				go.transform.localScale=Vector3.one;
				RoomNodeCtl rnc=go.GetComponent<RoomNodeCtl>();
				ButtonMsgObj bmo=go.GetComponent<ButtonMsgObj>();
				bmo.value=rnc;
				bmo.target=this.gameObject;
				rnc_ary[i,o]=rnc;
				rnc.id=new Point(i,o);
				rnc_lst.Add(rnc);
				
				//SpriteRenderer sr=go.transform.Find("spt").GetComponent<SpriteRenderer>();
				
				if(!(i==l_x && o==l_y))
				{
					//分布随机右或下连接点
					bool r=(GameHelp.Random());
					bool d=(GameHelp.Random());
					if(r==false && d==false){if((GameHelp.Random())){r=true;}else{d=true;}}
					
					if(i==l_x)
					{
						r=false;
						if(!rnc_ary[e_x,o].right)d=true;	
					}
					
					if(o==l_y)
					{
						d=false;
						if(!rnc_ary[i,e_y].down)r=true;	
					}
					
					CreateLine(r,d,rnc);
					rnc_ary[i,o].right=r;rnc_ary[i,o].down=d;
				}
				//添加到剩余ids
				lSid.Add(new Point(i,o));
			}
		}
		
		//补位 能够减少通道计算损耗--------------------------------------------------
		//右下角 
		if(rnc_ary[mapSize.x-1,mapSize.y-2].down==false && rnc_ary[mapSize.x-2,mapSize.y-1].right==false)
		{
			if(GameHelp.Random())
			{
				rnc_ary[mapSize.x-1,mapSize.y-2].down=true;
			}
			else
			{
				rnc_ary[mapSize.x-2,mapSize.y-1].right=true;
			}
		}
		
		//右上角
		if(rnc_ary[mapSize.x-2,0].right==false && rnc_ary[mapSize.x-1,0].down==false)
		{
			if(GameHelp.Random())
			{
				rnc_ary[mapSize.x-2,0].right=true;
			}
			else
			{
				rnc_ary[mapSize.x-1,0].down=true;
			}
		}
		
		//相机锁定pre位置
		//cam.transform.localPosition=rnc_ary[(int)pre.x,(int)pre.y].transform.parent.localPosition;
		
		//临时ids总通道集
		List<List<Point>>allSid=new List<List<Point>>();

		//搜索出所有的通道集
		while(lSid.Count>0)
		{
			RoomNodeCtl rnc=rnc_ary[lSid[0].x,lSid[0].y];
			hSid=new List<Point>();
			
			hSid.Add(rnc.id);
			Find(rnc);
			allSid.Add(hSid);
			RemoveSid(hSid);

			//给找到的通道染色
			Color c=new Color(Random.Range(0.5f,1f),Random.Range(0.5f,1f),Random.Range(0.5f,1f));
			foreach(Point id in hSid)
			{
				rnc_ary[id.x,id.y].spt.color=c;
			}
		}
		
		//选取维度最高通道作为主要通道
		int max=int.MinValue;
		List<Point> mainSid=null;
		int mainSidId=0;
		for(int i=0;i<allSid.Count;i++)
		{
			if(allSid[i].Count>max)
			{
				max=allSid[i].Count;
				mainSidId=i;
			}
		}
	
		//从全通道列表中移除主要通道
		mainSid=allSid[mainSidId];
		allSid.RemoveAt(mainSidId);

		//给主通道染色
		foreach(Point id in mainSid)
		{
			rnc_ary[id.x,id.y].spt.color=new Color(0,0.8f,1f);
		}

		//剩余通道与主要通道进行融合
		int e=0;
		Debug.Log("剩余通道数量:"+allSid.Count);

		while(allSid.Count>0)
		{
			//寻找目前可以连接到主通道的剩余通道
			List<Point>rnc_str = allSid[e];
			Debug.Log("通道["+e+"]数量:"+rnc_str.Count);
		/*	foreach(Point id in rnc_str)
			{
				rnc_ary[id.x,id.y].spt.color=new Color(1f,1f,0.5f);
			}*/

			for(int o=0;o<rnc_str.Count;o++)
			{
				Point id=rnc_str[o];
				List<NodeConnect> nc_lst=new List<NodeConnect>();

				Debug.Log("id:"+id.ToString());

				//查找与主通道可以连接的接口
				if(id.x<mapSize.x-1){if(mainSid.Contains(new Point(id.x+1,id.y)))nc_lst.Add(new NodeConnect(id.x,id.y,NodeConnect.TYPE.RIGHT));}
				if(id.y<mapSize.y-1){if(mainSid.Contains(new Point(id.x,id.y+1)))nc_lst.Add(new NodeConnect(id.x,id.y,NodeConnect.TYPE.UP));}
				if(id.x>0){if(mainSid.Contains(new Point(id.x-1,id.y)))nc_lst.Add(new NodeConnect(id.x,id.y,NodeConnect.TYPE.LEFT));}
				if(id.y>0){if(mainSid.Contains(new Point(id.x,id.y-1)))nc_lst.Add(new NodeConnect(id.x,id.y,NodeConnect.TYPE.DOWN));}

				//存在至少一个接口
				if(nc_lst.Count>0)
				{
					Debug.Log("接口数量:"+nc_lst.Count);
	

					//取任意一个连接位
					NodeConnect nc=nc_lst[GameHelp.Random(0,nc_lst.Count-1)];
					Debug.Log(nc.id.ToString()+"  "+ nc.type);
					//连接可连接位
					if(nc.type==NodeConnect.TYPE.RIGHT)
					{
						rnc_ary[nc.id.x,nc.id.y].right=true;
					}
					else if(nc.type==NodeConnect.TYPE.LEFT)
					{
						rnc_ary[nc.id.x-1,nc.id.y].right=true;
					}
					else if(nc.type==NodeConnect.TYPE.DOWN)
					{
						rnc_ary[nc.id.x,nc.id.y-1].down=true;
					}
					else if(nc.type==NodeConnect.TYPE.UP)
					{
						rnc_ary[nc.id.x,nc.id.y].down=true;
					}

					//将当前通道添加到主通道
					AddSid(rnc_str,mainSid);

					//从剩余通道中移除当前通道
					allSid.RemoveAt(e);
					break;
				}

			}

			e++;
			if(e>=allSid.Count){e=0;}

		}
		
		DistributionRoomType();
		
		//GetDepth(pre,pre,out aim);
		/*
		Transform aim_trs= rnc_ary[(int)aim.x,(int)aim.y].transform;
		Transform pre_trs= rnc_ary[(int)pre.x,(int)pre.y].transform;
		
		pre_trs.GetComponent<SpriteRenderer>().color=new Color(1f,0.65f,0.0f,2);
		pre_trs.parent.transform.Find("spt").localScale=new Vector3(96,96,1);
		
		aim_trs.GetComponent<SpriteRenderer>().color=new Color(0.15f,1,0.25f,2);
		aim_trs.parent.transform.Find("spt").localScale=new Vector3(96,96,1);
		*/
		//cam_m.SetLocalAim(Vector3.zero);
	}

	///<summary>从剩余lids中移除临时找到的ids<summary>
	void RemoveSid(List<Point>sid)
	{
		for(int i=0;i<sid.Count;i++)
		{
			int id=SidContainsPoint(sid[i],lSid);
			if(id!=-1)
			{
				lSid.RemoveAt(id);
			//	Debug.Log("remove at "+i);
			}else Debug.Log("No Find!");
		}
	}

	///<summary>为某个列表添加另一列表的节点<summary>
	void AddSid(List<Point>add_sid,List<Point>main_sid)
	{
		Debug.Log("add_sid.Count:"+add_sid.Count);
		Debug.Log("main_sid.Count:"+main_sid.Count);
		for(int i=0;i<add_sid.Count;i++)
		{
			main_sid.Add(add_sid[i]);
		}
	}
	

	///<summary>查找某个id在列表中<summary>
	int SidContainsPoint(Point id,List<Point>sid)
	{
		for(int i=0;i<sid.Count;i++)
		{
			if(sid[i]==id)
			{
				return i;
			}
		}
		return -1;
	}

	///<summary>查找某个id在列表中<summary>
	bool ContainsPoint(Point id,List<Point>sid)
	{
		return (SidContainsPoint(id,sid)!=-1);
	}

	///<summary>获取四面连结节点的列表<summary>
	List<RoomNodeCtl> GetConnect(RoomNodeCtl rnc)
	{
		List<RoomNodeCtl> rnc_lst=new List<RoomNodeCtl>();

		if(rnc.id.x<mapSize.x-1)if(rnc.right)rnc_lst.Add(rnc_ary[rnc.id.x+1,rnc.id.y]);
		if(rnc.id.y<mapSize.y-1)if(rnc.down)rnc_lst.Add(rnc_ary[rnc.id.x,rnc.id.y+1]);
		if(rnc.id.x>0)if(rnc_ary[rnc.id.x-1,rnc.id.y].right)rnc_lst.Add(rnc_ary[rnc.id.x-1,rnc.id.y]);
		if(rnc.id.y>0)if(rnc_ary[rnc.id.x,rnc.id.y-1].down)rnc_lst.Add(rnc_ary[rnc.id.x,rnc.id.y-1]);
		
		return rnc_lst;
	}

	
	List<Point> GetConnect(Point p)
	{
		List<Point>hfs=new List<Point>();
		if(p.x<mapSize.x-1)if(rnc_ary[p.x,p.y].right)hfs.Add(new Point(p.x+1,p.y));
		if(p.y<mapSize.y-1)if(rnc_ary[p.x,p.y].down)hfs.Add(new Point(p.x,p.y+1));
		if(p.x>0)if(rnc_ary[p.x-1,p.y].right)hfs.Add(new Point(p.x-1,p.y));
		if(p.y>0)if(rnc_ary[p.x,p.y-1].down)hfs.Add(new Point(p.x,p.y-1));
		return hfs;
	}

	///<summary>深度搜索 添加到已有列表<summary>
	void Find(RoomNodeCtl rnc)
	{
		List<RoomNodeCtl> rnc_lst=GetConnect(rnc);
		
		for(int i=0;i<rnc_lst.Count;i++)
		{
			Point id=rnc_lst[i].id;
			//若是新的元素
			if(!hSid.Contains(id))
			{
				hSid.Add(id);
				Find(rnc_lst[i]);
			}
		}
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			OnStart();
		}
	}
}
