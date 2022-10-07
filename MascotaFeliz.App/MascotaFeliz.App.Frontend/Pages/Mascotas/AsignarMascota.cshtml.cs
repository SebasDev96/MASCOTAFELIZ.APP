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
    public class AsignarMascotaModel : PageModel
    {
        private readonly IRepositorioMascota _repoMascota;
        private readonly IRepositorioDueno _repoDueno;
        private readonly IRepositorioVeterinario _repoVeterinario;
        [BindProperty]

        public Mascota mascota {get;set;}
        public IEnumerable<Veterinario> veterinarios {get;set;}  
        public IEnumerable<Dueno> duenos {get;set;}  

        

        public AsignarMascotaModel()
        {
            this._repoMascota = new RepositorioMascota(new Persistencia.AppContext());
            this._repoDueno = new RepositorioDueno(new Persistencia.AppContext());
            this._repoVeterinario=new RepositorioVeterinario (new Persistencia.AppContext());

        }
        public IActionResult OnGet(int mascotaId)
        {
            mascota = _repoMascota.GetMascota(mascotaId);
            veterinarios = _repoVeterinario.GetAllVeterinarios();
            duenos = _repoDueno.GetAllDuenos();

            

            return Page();
        }
         public IActionResult OnPost(int vet, int d)
         {
             if(!ModelState.IsValid)
            {
                return Page();
            }
           
          if (vet > 0)
          {
             _repoMascota.AsignarVeterinario(mascota.Id, vet);
          }
          if (d > 0)
          {
            _repoMascota.AsignarDueno(mascota.Id, d);
          }
            
             
            return RedirectToPage("./ListarMascotas");
         }
         
    }
}
