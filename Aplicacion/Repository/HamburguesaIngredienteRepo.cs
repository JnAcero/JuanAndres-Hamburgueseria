using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository
{
    public class HamburguesaIngredienteRepo : GenericRepo<HamburguesaIngrediente>, IHamburguesaIngrediente
    {
        public HamburguesaIngredienteRepo(DbAppContext context) : base(context)
        {
        }
    }
}