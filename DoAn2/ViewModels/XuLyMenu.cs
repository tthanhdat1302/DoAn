using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAn2.Models;

namespace DoAn2.ViewModels
{
    
    public class XuLyMenu
    {
        BOOKSTOREEntities db = new BOOKSTOREEntities();
        public List<Menu> ListByGroupId(int groupId)
        {
            return db.Menus.Where(x => x.TypeID == groupId && x.Status == true)
                .OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}