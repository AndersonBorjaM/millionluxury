using System.Linq.Expressions;
using Million.Domain.Abstractions;
using Million.Domain.Shared;

namespace Million.Domain.Properties.Specifications
{
    public class PropertySpecification : BaseSpecification<Property>
    {
        public override Expression<Func<Property, bool>> ToExpression()
        {
            return prop => true;
        }
    }

    public class PropertyNameSpecification : BaseSpecification<Property>
    {
        private readonly Name _name;

        public PropertyNameSpecification(Name name)
        {
            _name = name;
        }

        public override Expression<Func<Property, bool>> ToExpression()
        {
            return prop => prop.Name == _name;
        }
    }

    public class PropertyAddressSpecification : BaseSpecification<Property>
    {
        private readonly Address _address;

        public PropertyAddressSpecification(Address address)
        {
            _address = address;
        }

        public override Expression<Func<Property, bool>> ToExpression()
        {
            return prop => prop.Address == _address;
        }
    }

    public class PropertyYearSpecification : BaseSpecification<Property>
    {
        private readonly Year _year;

        public PropertyYearSpecification(Year year)
        {
            _year = year;
        }

        public override Expression<Func<Property, bool>> ToExpression()
        {
            return prop => prop.Year == _year;
        }
    }

    public class PropertyCodeInternalSpecification : BaseSpecification<Property>
    {
        private readonly CodeInternal _codeInternal;

        public PropertyCodeInternalSpecification(CodeInternal codeInternal)
        {
            _codeInternal = codeInternal;
        }

        public override Expression<Func<Property, bool>> ToExpression()
        {
            return prop => prop.CodeInternal == _codeInternal;
        }
    }
}
