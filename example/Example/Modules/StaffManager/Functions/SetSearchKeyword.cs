﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Retyped;
using Library;
using Ajax.Admin;
namespace Modules.StaffManager.Functions
{
    /// <summary>
    ///  Gán session keyword để phục vụ cho tìm kiếm
    /// </summary>
    public class SetSearchKeyword : Function
    {
        #region "class"

        private Library.Browser.Data _data;

        #endregion

        #region "var"

        public SetKeywordForSearch ajax;
        public string keyword;
        #endregion

        public SetSearchKeyword()
        {
            _data = new Library.Browser.Data();
        }

        public override void VariablesInit()
        {
            keyword = _data.getValueById<string>("Email");
        }

        public override void Execute()
        {
            VariablesInit();
            ajaxRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void ajaxRequest()
        {
            ajax = new SetKeywordForSearch()
            {
                Async = true,
                data = new
                {
                    keyword = keyword
                }.ToDynamic()
            };

            ajax.success = setSearchKeyword_ok;
            ajax.error = setSearchKeyword_error;
            ajax.Request();
        }

        /// <summary>
        ///  Lỗi xảy ra khi gán session keyword
        /// </summary>
        /// <param name="jqXHR"></param>
        /// <param name="textStatus"></param>
        /// <param name="errorThrown"></param>
        private void setSearchKeyword_error(jquery.JQuery.jqXHR<object> jqXHR, jquery.JQuery.Ajax.ErrorTextStatus textStatus, string errorThrown)
        {
            dom.alert("Bị lỗi rồi");
        }

        /// <summary>
        ///  Nạp lại grid danh sách nhân viên mỗi khi đã gán session keyword thành công
        /// </summary>
        /// <param name="data"></param>
        /// <param name="textStatus"></param>
        /// <param name="jqXHR"></param>
        private void setSearchKeyword_ok(object data, jquery.JQuery.Ajax.SuccessTextStatus textStatus, jquery.JQuery.jqXHR<object> jqXHR)
        {
            var KendoGrid = _data.getKendoGrid("grid");

            _data.kendGridReloadData(KendoGrid);
        }

    }
}