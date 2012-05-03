using System.Collections.Generic;
using System.Web.Mvc;
using StructureMap;

namespace Guidelines.Ioc.Adaptors
{
    public class StructureMapFilterProvider : FilterAttributeFilterProvider
    {
        public StructureMapFilterProvider(IContainer container)
        {
            _container = container;
        }

        private readonly IContainer _container;

        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);

            foreach (var filter in filters)
            {
                _container.BuildUp(filter.Instance);
            }

            return filters;
        }
    }

}