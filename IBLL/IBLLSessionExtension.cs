
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL
{
	public partial interface IBLLSession
    {
		IsysdiagramsBLL IsysdiagramsBLL{get;set;}
		IT001账号表BLL IT001账号表BLL{get;set;}
		IT002验证表BLL IT002验证表BLL{get;set;}
		IT003用户角色表BLL IT003用户角色表BLL{get;set;}
		IT004社团信息表BLL IT004社团信息表BLL{get;set;}
		IT005票务表BLL IT005票务表BLL{get;set;}
		IT006店铺信息表BLL IT006店铺信息表BLL{get;set;}
		IT007店铺货物表BLL IT007店铺货物表BLL{get;set;}
		IT008海报信息表BLL IT008海报信息表BLL{get;set;}
		IT009社团账号表BLL IT009社团账号表BLL{get;set;}
    }

}