
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;

namespace DAL
{
	public partial class DBSession:IDAL.IDBSession
    {
		#region 01 数据接口 IsysdiagramsDAL
		IsysdiagramsDAL isysdiagramsDAL;
		public IsysdiagramsDAL IsysdiagramsDAL
		{
			get
			{
				if(isysdiagramsDAL==null)
					isysdiagramsDAL= new sysdiagramsDAL();
				return isysdiagramsDAL;
			}
			set
			{
				isysdiagramsDAL= value;
			}
		}
		#endregion
        
		#region 02 数据接口 IT001账号表DAL
		IT001账号表DAL iT001账号表DAL;
		public IT001账号表DAL IT001账号表DAL
		{
			get
			{
				if(iT001账号表DAL==null)
					iT001账号表DAL= new T001账号表DAL();
				return iT001账号表DAL;
			}
			set
			{
				iT001账号表DAL= value;
			}
		}
		#endregion

		#region 03 数据接口 IT002验证表DAL
		IT002验证表DAL iT002验证表DAL;
		public IT002验证表DAL IT002验证表DAL
		{
			get
			{
				if(iT002验证表DAL==null)
					iT002验证表DAL= new T002验证表DAL();
				return iT002验证表DAL;
			}
			set
			{
				iT002验证表DAL= value;
			}
		}
		#endregion

		#region 04 数据接口 IT003用户角色表DAL
		IT003用户角色表DAL iT003用户角色表DAL;
		public IT003用户角色表DAL IT003用户角色表DAL
		{
			get
			{
				if(iT003用户角色表DAL==null)
					iT003用户角色表DAL= new T003用户角色表DAL();
				return iT003用户角色表DAL;
			}
			set
			{
				iT003用户角色表DAL= value;
			}
		}
		#endregion

		#region 05 数据接口 IT004社团信息表DAL
		IT004社团信息表DAL iT004社团信息表DAL;
		public IT004社团信息表DAL IT004社团信息表DAL
		{
			get
			{
				if(iT004社团信息表DAL==null)
					iT004社团信息表DAL= new T004社团信息表DAL();
				return iT004社团信息表DAL;
			}
			set
			{
				iT004社团信息表DAL= value;
			}
		}
		#endregion

		#region 06 数据接口 IT005票务表DAL
		IT005票务表DAL iT005票务表DAL;
		public IT005票务表DAL IT005票务表DAL
		{
			get
			{
				if(iT005票务表DAL==null)
					iT005票务表DAL= new T005票务表DAL();
				return iT005票务表DAL;
			}
			set
			{
				iT005票务表DAL= value;
			}
		}
		#endregion

		#region 07 数据接口 IT006店铺信息表DAL
		IT006店铺信息表DAL iT006店铺信息表DAL;
		public IT006店铺信息表DAL IT006店铺信息表DAL
		{
			get
			{
				if(iT006店铺信息表DAL==null)
					iT006店铺信息表DAL= new T006店铺信息表DAL();
				return iT006店铺信息表DAL;
			}
			set
			{
				iT006店铺信息表DAL= value;
			}
		}
		#endregion

		#region 08 数据接口 IT007店铺货物表DAL
		IT007店铺货物表DAL iT007店铺货物表DAL;
		public IT007店铺货物表DAL IT007店铺货物表DAL
		{
			get
			{
				if(iT007店铺货物表DAL==null)
					iT007店铺货物表DAL= new T007店铺货物表DAL();
				return iT007店铺货物表DAL;
			}
			set
			{
				iT007店铺货物表DAL= value;
			}
		}
		#endregion

		#region 09 数据接口 IT008海报信息表DAL
		IT008海报信息表DAL iT008海报信息表DAL;
		public IT008海报信息表DAL IT008海报信息表DAL
		{
			get
			{
				if(iT008海报信息表DAL==null)
					iT008海报信息表DAL= new T008海报信息表DAL();
				return iT008海报信息表DAL;
			}
			set
			{
				iT008海报信息表DAL= value;
			}
		}
		#endregion

		#region 10 数据接口 IT009社团账号表DAL
		IT009社团账号表DAL iT009社团账号表DAL;
		public IT009社团账号表DAL IT009社团账号表DAL
		{
			get
			{
				if(iT009社团账号表DAL==null)
					iT009社团账号表DAL= new T009社团账号表DAL();
				return iT009社团账号表DAL;
			}
			set
			{
				iT009社团账号表DAL= value;
			}
		}
		#endregion

    }

}