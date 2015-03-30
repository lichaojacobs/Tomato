 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using IBLL;
using IDAL;
namespace BLL
{
	public partial class sysdiagramsBLL: BaseBLL<Model.sysdiagrams>,IsysdiagramsBLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IsysdiagramsDAL;
		}
    }
	public partial class T001账号表BLL: BaseBLL<Model.T001账号表>,IT001账号表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT001账号表DAL;
		}
    }
	public partial class T002验证表BLL: BaseBLL<Model.T002验证表>,IT002验证表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT002验证表DAL;
		}
    }
	public partial class T003用户角色表BLL: BaseBLL<Model.T003用户角色表>,IT003用户角色表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT003用户角色表DAL;
		}
    }
	public partial class T004社团信息表BLL: BaseBLL<Model.T004社团信息表>,IT004社团信息表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT004社团信息表DAL;
		}
    }
	public partial class T005票务表BLL: BaseBLL<Model.T005票务表>,IT005票务表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT005票务表DAL;
		}
    }
	public partial class T006店铺信息表BLL: BaseBLL<Model.T006店铺信息表>,IT006店铺信息表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT006店铺信息表DAL;
		}
    }
	public partial class T007店铺货物表BLL: BaseBLL<Model.T007店铺货物表>,IT007店铺货物表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT007店铺货物表DAL;
		}
    }
	public partial class T008海报信息表BLL: BaseBLL<Model.T008海报信息表>,IT008海报信息表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT008海报信息表DAL;
		}
    }
	public partial class T009社团账号表BLL: BaseBLL<Model.T009社团账号表>,IT009社团账号表BLL
    {
		public override void SetDAL()
		{
			idal = DBSession.IT009社团账号表DAL;
		}
    }
}