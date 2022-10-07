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
    public class ListarDuenosModel : PageModel
    {

        private static IRepositorioDueno _repoDueno = new RepositorioDueno(new Persistencia.AppContext());

        public IEnumerable<Dueno> ListarDuenos {get;set;}


        public void OnGet()
        {

            ListarDuenos = _repoDueno.GetAllDuenos();
        }
    }
}
    