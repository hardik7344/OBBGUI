using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OBB_Project.BusinessLogic;
using OBB_Project.DataAccessLayer;
using OBB_Project.Models;

namespace OBB_Project.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class os_loginController : ControllerBase
    {
        ManageUser objManageUser = new ManageUser();
        ManageRights objManageRights = new ManageRights();

        DataTable dt = new DataTable();
        string data = "";
        bool bflag = false;

        //login page(User/Admin)
        //GET:api/os_login/get_user_id/username
        [HttpGet("{username}")]
        public string get_user_id(string username)
        {
            data = objManageUser.get_user_id(username);
            return data;
        }

        //GET:api/os_login/os_login/username/password
        [HttpGet("{username}/{password}")]

        public bool os_login(string username,string password)
        {
            bflag = objManageUser.doUserLogin(username,password);
            return bflag;
        }

        //LoadMenuItem
        //GET:api/os_login/loadmenuitem
        [HttpGet()]
        public string loadmenuitem()
        {
            data = objManageRights.loadMenuItem();
            return data;
        }

        //GetMenuURL
        //GET:api/os_login/GetmenuURL/menuid
        [HttpGet("{menuid}")]
        public  string GetmenuURL(string menuid)
        {
            data = objManageRights.get_menu_url(menuid);
            return data;
        }

        //GetstartUp
        //GET:api/os_login/GetstartUp/userid
        [HttpGet("{userid}")]
        public string GetstartUp(string userid)
        {
            data = objManageUser.Get_startup(userid);
            return data;
        }


        //Dashboard
        //GET:api/os_login/Get_Dashboard/user_id
        [HttpGet("{user_id}")]
        public DataTable Get_Dashboard(string user_id)
        {
            DataTable dtgetdash = new DataTable();
            dtgetdash = objManageRights.Get_Dashbord_chats(user_id);
            return dtgetdash;
        }
    }
}