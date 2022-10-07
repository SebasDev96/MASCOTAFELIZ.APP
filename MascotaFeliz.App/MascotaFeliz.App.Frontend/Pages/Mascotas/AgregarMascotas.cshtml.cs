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
    public class AgregarMascotasModel : PageModel
    {

   
    private readonly IRepositorioMascota _repoMascota;
    private readonly IRepositorioHistoria _repoHistoria;
    [BindProperty]
    public Mascota mascota {get;set;}

    public AgregarMascotasModel()
    {
        this._repoMascota = new RepositorioMascota(new Persistencia.AppContext());
        this._repoHistoria = new RepositorioHistoria(new Persistencia.AppContext());
    }
    
        
        public IActionResult OnGet(int? mascotaId)
        {
            if(mascotaId.HasValue){
                mascota = _repoMascota.GetMascota(mascotaId.Value);
            }
            else
            {
                mascota=new Mascota();

            }
            if (mascota==null)
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
            if(!ModelState.IsValid)
            {
                return Page();
            }
            if(mascota.Id > 0)
            {
                mascota = _repoMascota.UpdateMascota(mascota);
            }
            else
            {
                _repoMascota.AddMascota(mascota);
                var historia = _repoHistoria.AddHistoria(new Historia{FechaInicial=DateTime.Today});
                mascota.Historia = historia;
                 mascota = _repoMascota.UpdateMascota(mascota);
            }
            return RedirectToPage("./ListarMascotas");
        }
    
    }
}
