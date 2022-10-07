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
    public class DetallesMascotaModel : PageModel
    {
        private static IRepositorioMascota _repoMascota = new RepositorioMascota(new Persistencia.AppContext());
        
        public Mascota mascota {get;set;}


        public IActionResult OnGet(int mascotaId)
        {
            mascota = _repoMascota.GetMascota(mascotaId);
            if(mascota == null)
            {
                return RedirectToPage("./NotFound");
            }
            else
            {
                if (mascota.Veterinario== null)
                {
                    mascota.Veterinario=new Veterinario();
                }
                if (mascota.Dueno==null)
                {
                    mascota.Dueno=new Dueno();
                }
                return Page();
            }
         }
    }
}
