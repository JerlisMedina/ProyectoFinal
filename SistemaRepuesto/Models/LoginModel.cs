using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaRepuesto.Models
{
    public class LoginModel
    {
        [Key]
        public int LoginId { get; set; }

        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nombre")]
        public string PersonalName { get; set; }

        [Required(ErrorMessage = "El Campo {0} Es Requerido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Role ID")]
        public int? LoginRoleID { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }

        [Display(Name = "Activo")]
        public bool LoginEstatus { get; set; }
    }
}