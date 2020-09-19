using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaRepuesto.Models
{
    public class MenuModel
    {

        [Display(Name = "Menu ID")]
        [Key]
        public int MainMenuID { get; set; }

        [Display(Name = "Menu")]
        public string MainDescripcion { get; set; }

        [Display(Name = "Activo")]
        public bool MainEstatus { get; set; }

        [Display(Name = "Sub ID")]
        public int SubID { get; set; }

        [Display(Name = "Sub Menu")]
        public string SubDescripcion { get; set; }

        [Display(Name = "Controller")]
        public string SubController { get; set; }

        [Display(Name = "Action")]
        public string SubAction { get; set; }

        [Display(Name = "Role ID")]
        public int? RoleId { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}