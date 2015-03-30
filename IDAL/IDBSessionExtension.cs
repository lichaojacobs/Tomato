
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
	public partial interface IDBSession
    {
		IsysdiagramsDAL IsysdiagramsDAL{get;set;}
		IT001账号表DAL IT001账号表DAL{get;set;}
		IT002验证表DAL IT002验证表DAL{get;set;}
		IT003用户角色表DAL IT003用户角色表DAL{get;set;}
		IT004社团信息表DAL IT004社团信息表DAL{get;set;}
		IT005票务表DAL IT005票务表DAL{get;set;}
		IT006店铺信息表DAL IT006店铺信息表DAL{get;set;}
		IT007店铺货物表DAL IT007店铺货物表DAL{get;set;}
		IT008海报信息表DAL IT008海报信息表DAL{get;set;}
		IT009社团账号表DAL IT009社团账号表DAL{get;set;}
    }

}