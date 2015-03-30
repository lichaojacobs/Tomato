using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace ClassLibrary
{
  public class ManagePerson
    {
      /// <summary>
      /// 获取所有用户
      /// </summary>
      /// <returns></returns>
      public List<UserInfo> getALLNormalUser()
      {
          

          var t001 = operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email != null);
          List<ViewModel.UserInfo> allUser = new List<ViewModel.UserInfo>();
          foreach (var i in t001)
          {
              UserInfo userInfor = new UserInfo();
              userInfor.username = i.UserName;
              userInfor.nickname = i.NickName;
              userInfor.email = i.Email;
              userInfor.password = i.Password;
              userInfor.Roles = new UserAuthtication().GetRoles(i.Email);
              userInfor.UserState = i.UserState;
              allUser.Add(userInfor);
 
          }

          return allUser;
      }


      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public int getUserCount()
      {

          return operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email != null).Count();
 
      }
      /// <summary>
      /// 封锁用户的具体函数
      /// </summary>
      /// <returns></returns>
      public bool blockUser(string email)
      {
           
          var t006 = operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email == email).FirstOrDefault();
          if (t006.UserState == "已激活")
          {
              t006.UserState = "未激活";
          }
          else
          {
              t006.UserState = "已激活";
          
          }
          if (operateContext.BLLSession.IT001账号表BLL.Modify(t006, "UserState") == 1)
          {
              return true;
          }
         
          return false;
 
      }
      /// <summary>
      /// 根据指定账户删除用户
      /// </summary>
      /// <param name="email"></param>
      /// <returns></returns>
      public bool deleteUser(string email)
      {
         
          //var t006 = operateContext.BLLSession.IT001账号表BLL.GetListBy(m => m.Email == email).FirstOrDefault();

          ///获得店铺的id
          var shop = operateContext.BLLSession.IT006店铺信息表BLL.GetListBy(m => m.Owners == email).FirstOrDefault();
          if (shop != null)
          {
              int shopid = shop.MallID;
              ///批量删除
              try
              {
                  operateContext.BLLSession.IT007店铺货物表BLL.DelBy(m => m.GoodID == shopid);
                  operateContext.BLLSession.IT006店铺信息表BLL.DelBy(m => m.Owners == email);
                  operateContext.BLLSession.IT003用户角色表BLL.DelBy(m => m.Email == email);
                  operateContext.BLLSession.IT001账号表BLL.DelBy(m => m.Email == email);
                  return true;
              }
              catch (Exception)
              {

                  throw;
              }


          }
          else
          {
              try
              {
                   operateContext.BLLSession.IT006店铺信息表BLL.DelBy(m => m.Owners == email);
                  operateContext.BLLSession.IT003用户角色表BLL.DelBy(m => m.Email == email);
                  operateContext.BLLSession.IT001账号表BLL.DelBy(m => m.Email == email);
                  return true;
              }
              catch (Exception)
              {
                  
                  throw;
              }
 
          }                        
 
      }
    }
}
