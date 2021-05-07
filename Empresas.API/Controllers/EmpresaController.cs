using Empresas.Infra.Data.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empresas.Dominio.Models;

namespace Empresas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaRepositorio empresaRepositorio;
        public EmpresaController()
        {
            empresaRepositorio = new EmpresaRepositorio();
        }
        [HttpGet]
        public IEnumerable<Empresa> Get()
        {
            return empresaRepositorio.GetAll();
        }

        [HttpGet("{cnpj}")]
        public Empresa Get(string cnpj)
        {
            return empresaRepositorio.GetByCNPJ(cnpj);
        }

        [HttpPost]
        public void Post([FromBody] Empresa _empresa)
        {
            if (ModelState.IsValid)
            {
                empresaRepositorio.Add(_empresa);
            }
        }

        [HttpPut("{cnpj}")]
        public void Put(string cnpj, [FromBody] Empresa _empresa)
        {
            _empresa.CNPJ = cnpj;
            if (ModelState.IsValid)
            {
                empresaRepositorio.Update(_empresa);
            }
        }

        [HttpDelete]
        public void Delete(string cnpj)
        {
            empresaRepositorio.Delete(cnpj);
        }
    }
}
