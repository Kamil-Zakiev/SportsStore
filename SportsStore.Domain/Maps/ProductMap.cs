using NHibernate.Mapping.ByCode.Conformist;
using SportsStore.Domain.Entities;
using NHibernate.Mapping.ByCode;

namespace SportsStore.Domain.Maps
{
    public class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            Table("product");

            Id(p => p.Id, m =>
            {
                m.Column("id");
                m.Generator(Generators.Sequence, g => g.Params(new { sequence = "product_id_seq" }));
            });
            Property(p => p.Name, m => m.Column("name"));
            Property(p => p.Price, m => m.Column("price"));
            Property(p => p.Category, m => m.Column("category"));
            Property(p => p.Description, m => m.Column("description"));
            Property(p => p.ImageData, m => m.Column("image_data"));
            Property(p => p.ImageMimeType, m => m.Column("image_mime_type"));
        }
    }
}
