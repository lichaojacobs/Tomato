
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBLL;

namespace BLL
{
	public partial class BLLSession:IBLLSession
    {
		#region 01 业务接口 IsysdiagramsBLL
		IsysdiagramsBLL isysdiagramsBLL;
		public IsysdiagramsBLL IsysdiagramsBLL
		{
			get
			{
				if(isysdiagramsBLL==null)
					isysdiagramsBLL= new sysdiagramsBLL();
				return isysdiagramsBLL;
			}
			set
			{
				isysdiagramsBLL= value;
			}
		}
		#endregion

		#region 02 业务接口 IT001账号表BLL
		IT001账号表BLL iT001账号表BLL;
		public IT001账号表BLL IT001账号表BLL
		{
			get
			{
				if(iT001账号表BLL==null)
					iT001账号表BLL= new T001账号表BLL();
				return iT001账号表BLL;
			}
			set
			{
				iT001账号表BLL= value;
			}
		}
		#endregion

		#region 03 业务接口 IT002验证表BLL
		IT002验证表BLL iT002验证表BLL;
		public IT002验证表BLL IT002验证表BLL
		{
			get
			{
				if(iT002验证表BLL==null)
					iT002验证表BLL= new T002验证表BLL();
				return iT002验证表BLL;
			}
			set
			{
				iT002验证表BLL= value;
			}
		}
		#endregion

		#region 04 业务接口 IT003用户角色表BLL
		IT003用户角色表BLL iT003用户角色表BLL;
		public IT003用户角色表BLL IT003用户角色表BLL
		{
			get
			{
				if(iT003用户角色表BLL==null)
					iT003用户角色表BLL= new T003用户角色表BLL();
				return iT003用户角色表BLL;
			}
			set
			{
				iT003用户角色表BLL= value;
			}
		}
		#endregion

		#region 05 业务接口 IT004社团信息表BLL
		IT004社团信息表BLL iT004社团信息表BLL;
		public IT004社团信息表BLL IT004社团信息表BLL
		{
			get
			{
				if(iT004社团信息表BLL==null)
					iT004社团信息表BLL= new T004社团信息表BLL();
				return iT004社团信息表BLL;
			}
			set
			{
				iT004社团信息表BLL= value;
			}
		}
		#endregion

		#region 06 业务接口 IT005票务表BLL
		IT005票务表BLL iT005票务表BLL;
		public IT005票务表BLL IT005票务表BLL
		{
			get
			{
				if(iT005票务表BLL==null)
					iT005票务表BLL= new T005票务表BLL();
				return iT005票务表BLL;
			}
			set
			{
				iT005票务表BLL= value;
			}
		}
		#endregion

		#region 07 业务接口 IT006店铺信息表BLL
		IT006店铺信息表BLL iT006店铺信息表BLL;
		public IT006店铺信息表BLL IT006店铺信息表BLL
		{
			get
			{
				if(iT006店铺信息表BLL==null)
					iT006店铺信息表BLL= new T006店铺信息表BLL();
				return iT006店铺信息表BLL;
			}
			set
			{
				iT006店铺信息表BLL= value;
			}
		}
		#endregion

		#region 08 业务接口 IT007店铺货物表BLL
		IT007店铺货物表BLL iT007店铺货物表BLL;
		public IT007店铺货物表BLL IT007店铺货物表BLL
		{
			get
			{
				if(iT007店铺货物表BLL==null)
					iT007店铺货物表BLL= new T007店铺货物表BLL();
				return iT007店铺货物表BLL;
			}
			set
			{
				iT007店铺货物表BLL= value;
			}
		}
		#endregion

		#region 09 业务接口 IT008海报信息表BLL
		IT008海报信息表BLL iT008海报信息表BLL;
		public IT008海报信息表BLL IT008海报信息表BLL
		{
			get
			{
				if(iT008海报信息表BLL==null)
					iT008海报信息表BLL= new T008海报信息表BLL();
				return iT008海报信息表BLL;
			}
			set
			{
				iT008海报信息表BLL= value;
			}
		}
		#endregion

		#region 10 业务接口 IT009社团账号表BLL
		IT009社团账号表BLL iT009社团账号表BLL;
		public IT009社团账号表BLL IT009社团账号表BLL
		{
			get
			{
				if(iT009社团账号表BLL==null)
					iT009社团账号表BLL= new T009社团账号表BLL();
				return iT009社团账号表BLL;
			}
			set
			{
				iT009社团账号表BLL= value;
			}
		}
		#endregion

    }

}