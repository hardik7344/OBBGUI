using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    public class settingsController : Controller
    {
        ManageOrganization objManageOrganiz = new ManageOrganization();
        ManageOtherInformation objManageOtherInfo = new ManageOtherInformation();
        ManageGroup objManageGroup = new ManageGroup();
        ManageUser objManageUser = new ManageUser();
        ManageRights objManageRights = new ManageRights();
        ManageOUSettings objManageOUSettings = new ManageOUSettings();
        ManageExternalEntity objManageEntity = new ManageExternalEntity();
        DataTable dt1 = new DataTable();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        string result = "";
        string result1 = "";


        #region settings_organization_structure


        //GET: api/settings/Getleveldata
        [HttpGet()]
        public DataTable Getleveldata()
        {
            dt = objManageOrganiz.Get_Level();
            return dt;
        }

        //GET: api/settings/Getlevelid
        [HttpGet()]
        public DataTable Getlevelid()
        {
            dt = objManageOrganiz.Get_Level_id();
            return dt;
        }

        //POST: api/Settings/Addlevel
        [HttpPost()]
        public string Addlevel([FromBody] ParameterJSON objjson)
        {
            result = objManageOrganiz.insert_oulevel(objjson.user_json)
                ;
            return result;
        }

        //POST: api/Settings/Editlevel
        [HttpPost()]
        public string Editlevel([FromBody] ParameterJSON objjson)
        {
            result = objManageOrganiz.Update_level(objjson.user_json);
            return result;
        }

        //GET: api/Settings/BindlevelProperty/(levelid)
        [HttpGet("{levelid}")]
        public DataTable BindlevelProperty(string levelid)
        {
            dt = objManageOrganiz.Get_level_rights(levelid);
            return dt;
        }

        //POST: api/Settings/Updatelevelrights
        [HttpPost()]
        public string Updatelevelrights([FromBody] ParameterJSON objjson)
        {
            result = objManageOrganiz.Insert_Updatelevelrights(objjson.user_json);
            return result;
        }

        //GET: api/Settings/get_OU_Data_child_tree
        [HttpGet()]
        public string get_OU_Data_child_tree()
        {
            result = objManageOrganiz.Get_OU_Child_Data_Tree();
            return result;
        }

        //POST: api/Settings/Add_Branch_Unit_ou_data
        [HttpPost()]
        public DataTable Add_Branch_Unit_ou_data([FromBody] ParameterJSON objjson)
        {
            dt = objManageOrganiz.Add_Branch_Unit_ou_data(objjson.user_json);
            return dt;
        }

        //POST: api/Settings/add_new_ou_branch_unit
        [HttpPost()]
        public string add_new_ou_branch_unit([FromBody] ParameterJSON objjson)
        {
            result = objManageOrganiz.add_new_ou_branch_unit(objjson.user_json);
            return result;
        }

        //POST: api/Settings/getouname
        [HttpPost()]
        public DataTable getouname([FromBody] ParameterJSON objjson)
        {
            dt = objManageOrganiz.Add_Branch_Unit_ou_data(objjson.user_json);
            return dt;
        }

        //GET: api/Settings/ousystemlinkup
        [HttpPost()]
        public DataSet ousystemlinkup([FromBody] ParameterJSON objjson)
        {
            dt = objManageOrganiz.Add_Branch_Unit_ou_data(objjson.user_json);
            dt1 = objManageOrganiz.ousystemlinkup(objjson.user_json);
            // var data = new { ounameheader = dt, ousystem_linkup = dt1 };
            dt.TableName = "ounameheader";
            dt1.TableName = "ousystem_linkup";
            ds.Tables.Add(dt);
            ds.Tables.Add(dt1);
            return ds;
        }

        //POST: api/Settings/apply_ou_link_system
        [HttpPost()]
        public string apply_ou_link_system([FromBody] ParameterJSON objjson)
        {
            result = objManageOrganiz.apply_ou_link_system(objjson.user_json);
            return result;
        }

        //GET: api/Settings/get_ouparameterdata
        [HttpGet()]
        public DataTable get_ouparameterdata()
        {
            dt = objManageOtherInfo.get_parameterdata();
            return dt;
        }

        //POST: api/Settings/ouparamadd
        [HttpPost()]
        public string ouparamadd([FromBody] ParameterJSON objjson)
        {
            result = objManageOtherInfo.paramadd(objjson.user_json);
            return result;
        }

        //POST: api/Settings/otherinfo
        [HttpPost()]
        public string otherinfo([FromBody] ParameterJSON objjson)
        {
            result = objManageOtherInfo.otherinfo(objjson.user_json);
            return result;
        }

        //POST: api/Settings/show_otherinfo
        [HttpPost()]
        public DataTable show_otherinfo([FromBody] ParameterJSON objjson)
        {
            dt = objManageOtherInfo.show_otherinfo(objjson.user_json);
            return dt;
        }

        //POST: api/Settings/get_editotherinfodata
        [HttpPost()]
        public DataTable get_editotherinfodata([FromBody] ParameterJSON objjson)
        {
            dt = objManageOtherInfo.get_editotherinfodata(objjson.user_json);
            return dt;
        }

        //POST: api/Settings/Deleteinfo
        [HttpPost()]
        public string Deleteinfo([FromBody] ParameterJSON objjson)
        {
            result = objManageOtherInfo.Deleteinfo(objjson.user_json);
            return result;
        }

        //POST: api/Settings/Updateoudetails
        [HttpPost()]
        public string Updateoudetails([FromBody] ParameterJSON objjson)
        {
            result = objManageOrganiz.Updateoudetails(objjson.user_json);
            return result;
        }

        //POST: api/Settings/DeleteOU
        [HttpPost()]
        public string DeleteOU([FromBody] ParameterJSON objjson)
        {
            result = objManageOrganiz.DeleteOU(objjson.user_json);
            return result;
        }
        #endregion

        #region User Group Management

        //GET: api/Settings/ShowGroupmgmt
        [HttpGet()]
        public DataTable ShowGroupmgmt()
        {
            dt = objManageGroup.ShowGroupmgmt();
            return dt;
        }

        //GET: api/Settings/bindgroup_menudata
        [HttpGet()]
        public DataTable bindgroup_menudata()
        {
            dt = objManageGroup.bindgroup_data();
            return dt;
        }

        //POST: api/Settings/addgroup
        [HttpPost()]
        public string addgroup(string groupname, string groupdescription, string grouptype)
        {
            result = objManageGroup.addgroup(groupname, groupdescription, grouptype);
            return result;
        }

        //GET: api/Settings/get_group_data
        [HttpDelete("{groupid}")]
        public DataTable get_group_data(string groupid)
        {
            dt = objManageGroup.get_group_data(groupid);
            dt1 = objManageGroup.bindgroup_data();
            return dt;
            return dt1;
        }

        //GET: api/Settings/editgroup
        [HttpDelete("{groupname}/{groupdescription}/{grouptype}/{groupid}")]
        public string editgroup(string groupname, string groupdescription, string grouptype, string groupid)
        {
            result = objManageGroup.editgroup(groupname, groupdescription, grouptype, groupid);
            return result;
        }

        //GET: api/Settings/deletegroupdata
        [HttpDelete("{groupid}")]
        public string deletegroupdata(string groupid)
        {
            result = objManageGroup.deletegroupdata(groupid);
            return result;
        }
        #endregion

        #region User Type

        //GET: api/Settings/Get_product_type
        [HttpGet()]
        public DataTable Get_product_type()
        {
            dt = objManageOUSettings.Get_product_type();
            return dt;
        }

        //GET: api/Settings/Get_user_startup_screen
        [HttpPost()]
        public JsonResult Get_user_startup_screen([FromBody] ParameterJSON objjson)
        {
            DataTable dtgetstartup = new DataTable();
            dtgetstartup = objManageUser.Get_startup_page(objjson.user_json);
            return Json(data: dtgetstartup);
        }

        //GET: api/Settings/Get_usertype_datatable
        [HttpGet()]

        public DataTable Get_usertype_datatable()
        {
            dt = objManageOUSettings.Get_usertype_datatable();
            return dt;
        }

        //POST: api/Settings/save_usertype_data
        [HttpPost()]

        public string save_usertype_data([FromBody] ParameterJSON objjson)
        {
            result = objManageOUSettings.save_usertype_data(objjson.user_json);
            return result;
        }

        //GET: api/Settings/bindusertype_rightdata/2
        [HttpGet("{product_id}")]
        public string bindusertype_rightdata(string product_id)
        {
            result = objManageGroup.bindmenu_data(product_id);
            return result;
        }

        //POST: api/Settings/addusertyperights
        [HttpPost()]
        public string addusertyperights([FromBody] ParameterJSON objjson)
        {
            result = objManageRights.addusertyperights(objjson.user_json);
            return result;
        }

        //POST: api/Settings/get_editdata
        [HttpPost()]
        public JsonResult get_editdata([FromBody] ParameterJSON objjson)
        {
            DataTable dtgetstartup = new DataTable();
            dtgetstartup = objManageUser.Get_startup_page(objjson.user_json);
            DataTable dteditusertype = new DataTable();
            dteditusertype = objManageOUSettings.get_editdata(objjson.user_json);
            var data = new { getstartupedit = dtgetstartup, useredit = dteditusertype };
            return Json(data);
        }

        //POST: api/Settings/Delete_usertype
        [HttpPost()]
        public string Delete_usertype([FromBody] ParameterJSON objjson)
        {
            result = objManageOUSettings.Delete_usertype(objjson.user_json);
            return result;
        }
        #endregion

        #region User Management
        //GET: api/Settings/ShowUsermgmt
        [HttpGet()]
        public JsonResult ShowUsermgmt()
        {
            DataTable dtuser = new DataTable();
            string user_name = Configuration.username;
            dtuser = objManageUser.ShowUsermgmt();
            var data = new { showusermgmt = dtuser, get_user_name = user_name };
            return Json(data);
        }

        //GET: api/Settings/Get_user_type
        [HttpPost()]
        public DataTable Get_user_type([FromBody] ParameterJSON objjson)
        {
            dt = objManageUser.Get_user_type(objjson.user_json);
            return dt;
        }

        //POST: api/Settings/adduser
        [HttpPost()]
        public string adduser([FromBody] ParameterJSON objjson)
        {
            result = objManageUser.adduser(objjson.user_json);
            return result;
        }

        //GET: api/Settings/show_otherinfo_user
        [HttpGet("{userid}")]
        public DataTable show_otherinfo_user(string userid)
        {
            dt = objManageOtherInfo.show_otherinfo_user(userid);
            return dt;
        }

        //POST: api/Settings/otherinfo_user
        [HttpPost()]
        public string otherinfo_user([FromBody] ParameterJSON objjson)
        {
            result = objManageOtherInfo.otherinfo_user(objjson.user_json);
            return result;
        }

        //POST: api/Settings/get_user_data
        [HttpPost()]
        public JsonResult get_user_data([FromBody] ParameterJSON objjson)
        {
            string result = "";
            DataTable dtgetuser = new DataTable();
            DataTable dtgetusertype = new DataTable();
            dtgetuser = objManageUser.get_user_data(objjson.user_json);
            result = objManageUser.get_user_password(objjson.user_json);
            dtgetusertype = objManageUser.Get_user_type(objjson.user_json);
            var data = new { getuserdata = dtgetuser, getpassword = result, usertype = dtgetusertype };
            return Json(data);
        }

        //POST: api/Settings/edituser
        [HttpPost()]
        public string edituser([FromBody] ParameterJSON objjson)
        {
            result = objManageUser.edituser(objjson.user_json);
            return result;
        }

        //POST: api/Settings/set_reset_password
        [HttpPost()]
        public string set_reset_password([FromBody] ParameterJSON objjson)
        {
            result = objManageUser.set_reset_password(objjson.user_json);
            return result;
        }

        //POST: api/Settings/deleteuserdata
        [HttpPost()]
        public string deleteuserdata([FromBody] ParameterJSON objjson)
        {
            result = objManageUser.deleteuserdata(objjson.user_json);
            return result;
        }

        //POST: api/Settings/old_password_match
        [HttpPost()]
        public string old_password_match([FromBody] ParameterJSON objjson)
        {
            result = objManageUser.old_password_match(objjson.user_json);
            return result;
        }
        #endregion

        #region Link User

        //GET: api/Settings/Get_linkuser_datatable
        [HttpGet()]
        public JsonResult Get_linkuser_datatable()
        {
            dt = objManageOUSettings.Get_linkuser_datatable();
            return Json(dt);
        }

        //GET: api/Settings/linkgroup
        [HttpGet("{userid}")]
       
        public JsonResult linkgroup(string userid)
        {
            DataTable dtlinkgrp = new DataTable();
            dtlinkgrp = objManageOUSettings.linkgroup(userid);
            result = objManageOUSettings.get_user_name(userid);
            var data = new { linkgroup = dtlinkgrp, usernameheader = result };
            return Json(data);
        }

        //POST: api/Settings/apply_link_group
        [HttpPost()]
        public string apply_link_group([FromBody] ParameterJSON objjson)
        {
            result = objManageOUSettings.apply_link_group(objjson.user_json);
            return result;
        }


        //GET: api/Settings/GetOUChecked
        [HttpGet("{user_id}")]
        public JsonResult GetOUChecked(string user_id)
        {
            DataTable dtoucheck = new DataTable();
            dtoucheck = objManageOUSettings.GetOUChecked(user_id);
            result = objManageOUSettings.get_user_name(user_id);
            var data = new { ouchecked = dtoucheck, ouusernameheader = result };
            return Json(data);
        }

        //GET: api/Settings/GetOUdatatable
        [HttpGet("{userid}")]
        public JsonResult GetOUdatatable(string userid)
        {
            dt = objManageOUSettings.GetOUdatatable(userid);
            return Json(dt);
        }
        //POST: api/Settings/link_user_with_ou
        [HttpPost()]
        public JsonResult link_user_with_ou([FromBody] ParameterJSON objjson)
        {
            result = objManageOUSettings.link_user_with_ou(objjson.user_json);
            return Json(result);
        }
        //POST: api/Settings/Delete_link_ou
        [HttpPost()]
        public JsonResult Delete_link_ou([FromBody] ParameterJSON objjson)
        {
            result = objManageOUSettings.Delete_link_ou(objjson.user_json);
            return Json(result);
        }

        #endregion

        #region Manage User Rights

        //GET: api/Settings/binduser_groupdata
        [HttpGet()]
        public DataSet binduser_groupdata()
        {
            dt = objManageRights.binduser_groupdata();
            dt1 = objManageRights.get_groupname_rights();
            //var data = new { get_userdata = dtbindugrp, get_groupdata = dtgrprights };
            dt.TableName = "get_userdata";
            dt1.TableName = "get_groupdata";
            ds.Tables.Add(dt);
            ds.Tables.Add(dt1);
            return ds;
        }

        //POST: api/Settings/showuserdatatable
        [HttpPost()]
        public DataTable showuserdatatable([FromBody] ParameterJSON objjson)
        {
            dt = objManageRights.showuserdatatable(objjson.user_json);
            return dt;
        }

        //GET: api/Settings/binduser_rightdata/(userid)
        [HttpGet("{userid}")]
        public string binduser_rightdata(string userid)
        {
            result = objManageGroup.bindmenu_data(userid);
            result1 = objManageOUSettings.get_user_name(userid);
            //var data = new { node = result, usernameheader = user };
            string final_result = result + "" + result1;
            return final_result;
        }

        //POST: api/Settings/binduser_rightdata
        [HttpPost()]
        public string adduserrights([FromBody] ParameterJSON objjson)
        {
            result = objManageRights.adduserrights(objjson.user_json);
            return result;
        }
        #endregion

        #region Connect Parent


        //GET: api/Settings/get_parent_data
        [HttpGet()]
        public DataTable get_parent_data()
        {
            dt = objManageOUSettings.get_parent_data();
            return dt;
        }

        //POST: api/Settings/save_parent_data
        [HttpPost()]
        public string save_parent_data([FromBody] ParameterJSON objjson)
        {
            result = objManageOUSettings.save_parent_data(objjson.user_json);
            return result;
        }
        #endregion

        #region Child Installation


        //POST: api/Settings/save_child_data
        [HttpPost()]
        public string save_child_data([FromBody] ParameterJSON objjson)
        {
            result = objManageOUSettings.save_child_data(objjson.user_json);
            return result;
        }

        //GET: api/Settings/Get_child_datatable
        [HttpGet()]
        public DataTable Get_child_datatable()
        {
            dt = objManageOUSettings.Get_child_datatable();
            return dt;
        }

        //POST: api/Settings/Deletedetail
        [HttpPost()]
        public string Deletedetail([FromBody] ParameterJSON objjson)
        {
            result = objManageOUSettings.Deletedetail(objjson.user_json);
            return result;
        }
        #endregion

        #region External Entitiy 

        //GET: api/Settings/Showexternal_entity
        [HttpGet()]
        public JsonResult Showexternal_entity()
        {
            dt = objManageEntity.Showexternal_entity();
            return Json(dt);
        }

        //GET: api/Settings/Get_etype
        [HttpGet()]
        public JsonResult Get_etype()
        {
            dt = objManageEntity.Get_etype();
            return Json(dt);
        }

        //GET: api/Settings/Get_entity_type
        [HttpGet()]
        public JsonResult Get_entity_type()
        {
            result = objManageEntity.Get_entity_type();
            return Json(result);
        }

        //POST: api/Settings/add_external_entity
        [HttpPost()]
        public JsonResult add_external_entity([FromBody] ParameterJSON objjson)
        {
            result = objManageEntity.add_external_entity(objjson.user_json);
            return Json(result);
        }

        //POST: api/Settings/otherinfo_entity
        [HttpPost()]
        public JsonResult otherinfo_entity([FromBody] ParameterJSON objjson)
        {
            result = objManageOtherInfo.otherinfo_entity(objjson.user_json);
            return Json(result);
        }

        //GET: api/Settings/show_otherinfo_entity/(entityid)
        [HttpGet("{entityid}")]
        public JsonResult show_otherinfo_entity(string entityid)
        {
            dt = objManageOtherInfo.show_otherinfo_entity(entityid);
            return Json(dt);
        }

        //GET: api/Settings/entityuserlinkup/(entityid)
        [HttpGet("{entityid}")]
       
        public JsonResult entityuserlinkup(string entityid)
        {
            DataTable dteuserlink = new DataTable();
            result = objManageEntity.get_external_entityname(entityid);
            dteuserlink = objManageEntity.entityuserlinkup(entityid);
            var data = new { entityuser_linkup = dteuserlink, entitynameheader = result };
            return Json(data);
        }

        [HttpPost()]
        //POST: api/Settings/apply_entity_link_user
        public JsonResult apply_entity_link_user([FromBody] ParameterJSON objjson)
        {      
            result = objManageEntity.apply_entity_link_user(objjson.user_json);
            return Json(result);
        }

        //GET: api/Settings/GetOUCheckedentity/(entityid)
        [HttpGet("{entityid}")]
        public JsonResult GetOUCheckedentity(string entityid)
        {
            DataTable dteoucheck = new DataTable();
            result = objManageEntity.get_external_entityname(entityid);
            dteoucheck = objManageEntity.GetOUCheckedentity(entityid);
            var data = new { entityou_linkup = dteoucheck, entitynameheader = result };
            return Json(data);
        }
        //POST: api/Settings/link_entity_with_ou
        [HttpPost()]
        public JsonResult link_entity_with_ou([FromBody] ParameterJSON objjson)
        {
            result = objManageEntity.link_entity_with_ou(objjson.user_json);
            return Json(result);

        }

        //GET: api/Settings/get_entity_data/(entityid)
        [HttpGet("{entityid}")]
        public JsonResult get_entity_data(string entityid)
        {
            DataTable dtentity = new DataTable();
            DataTable dtgetetype = new DataTable();
            dtentity = objManageEntity.get_entity_datatable(entityid);
            dtgetetype = objManageEntity.Get_etype();
            var data = new { getentitydata = dtentity, get_entitytype = dtgetetype };
            return Json(data);
        }
        //POST: api/Settings/editentity
        [HttpPost()]
        public JsonResult editentity([FromBody] ParameterJSON objjson)
        {
            result = objManageEntity.editentity(objjson.user_json);
            return Json(result);
        }

        //POST: api/Settings/deleteentitydata
        [HttpPost()]
        public JsonResult deleteentitydata([FromBody] ParameterJSON objjson)
        {
            result = objManageEntity.deleteentitydata(objjson.user_json);
            return Json(result);
        }

        //GET: api/Settings/Get_entitytype_datatable
        [HttpGet()]
        public JsonResult Get_entitytype_datatable()
        {
            dt = objManageEntity.Get_entitytype_datatable();
            return Json(dt);
        }

        //POST: api/Settings/save_entitytype_data
        [HttpPost()]
        public JsonResult save_entitytype_data([FromBody] ParameterJSON objjson)
        {
            result = objManageEntity.save_entitytype_data(objjson.user_json);
            return Json(result);
        }

        //GET: api/Settings/get_editdata_entity/(serial_no)
        [HttpGet("{serial_no}")]
        public JsonResult get_editdata_entity(string serial_no)
        {
            dt = objManageEntity.get_editdata_entity(serial_no);
            return Json(dt);
        }

        //POST: api/Settings/Delete_entitytype
        [HttpPost()]
        public JsonResult Delete_entitytype([FromBody] ParameterJSON objjson)
        {
            result = objManageEntity.Delete_entitytype(objjson.user_json);
            return Json(result);
        }
        #endregion
    }
}