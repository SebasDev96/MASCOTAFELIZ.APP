using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MascotaFeliz.App.Dominio;
using MascotaFeliz.App.Persistencia;

namespace MascotaFeliz.App.Frontend.Pages
{
    public class EliminarDuenoModel : PageModel
    {
        
        private readonly IRepositorioDueno _repoDueno;
        private readonly IRepositorioMascota _repoMascota;
         [BindProperty]
        public Dueno dueno {get;set;}
        public Mascota mascota {get;set;}


       public EliminarDuenoModel()
        {
            this._repoDueno = new RepositorioDueno(new Persistencia.AppContext());
            this._repoMascota = new RepositorioMascota(new Persistencia.AppContext());
        }


        public IActionResult OnGet(int duenoId)
        {
            dueno = _repoDueno.GetDueno(duenoId);
            if(dueno == null)
            {
                return RedirectToPage("./NotFound");
                
            }
            else
            {
                return Page();
            }
         }

         public IActionResult OnPost()
         {
             
            if (dueno.Id > 0)
            {
                
                var mascotas = _repoMascota.GetMascotaPorDueno(dueno.Id);
                if (mascotas.Any())
            {
                foreach (var m in mascotas)
                {
                    m.Dueno=null;
                    _repoMascota.UpdateMascota(m);
                  
                }
            }
                _repoDueno.DeleteDueno(dueno.Id);
            }
          
            
            return RedirectToPage("./ListarDuenos");
         }
    }
}
