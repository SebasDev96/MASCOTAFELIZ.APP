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
    public class EliminarVeterinarioModel : PageModel
    {

        private readonly IRepositorioVeterinario _repoVeterinario;
        private readonly IRepositorioMascota _repoMascota;
         [BindProperty]
        public Veterinario veterinario {get;set;}
        public Mascota mascota {get;set;}


       public EliminarVeterinarioModel()
        {
            this._repoVeterinario = new RepositorioVeterinario(new Persistencia.AppContext());
            this._repoMascota = new RepositorioMascota(new Persistencia.AppContext());
        }


        public IActionResult OnGet(int veterinarioId)
        {
            veterinario = _repoVeterinario.GetVeterinario(veterinarioId);
            if(veterinario == null)
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
             
            if (veterinario.Id > 0)
            {
                
                var mascotas = _repoMascota.GetMascotaPorVeterinario(veterinario.Id);
                if (mascotas.Any())
            {
                foreach (var m in mascotas)
                {
                    m.Veterinario=null;
                    _repoMascota.UpdateMascota(m);
                  
                }
            }
                _repoVeterinario.DeleteVeterinario(veterinario.Id);
                //validar que el veterinario no este en lista de mascotas attmilu
            }
          
            
            return RedirectToPage("./ListarVeterinarios");
         }
    }
}
