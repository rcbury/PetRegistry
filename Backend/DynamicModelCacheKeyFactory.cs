using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS_PetRegistry.Models;

namespace PIS_PetRegistry.Backend
{

    /// <summary>
    /// xz che eto takoe 
    /// </summary>
    public class DynamicModelCacheKeyFactoryDesignTimeSupport : IModelCacheKeyFactory
    {
        public object Create(DbContext context, bool designTime)
            => context is RegistryPetsContext dynamicContext
                ? (context.GetType(), Authorization.AuthorizedUserDto == null ? 0 : Authorization.AuthorizedUserDto.Id, designTime)
                : (object)context.GetType();

        public object Create(DbContext context)
            => Create(context, false);
    }
}
