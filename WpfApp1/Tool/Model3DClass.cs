using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WpfApp1.Tool
{
    public class ModelVisual3DWithName : ModelVisual3D, ICloneable
    {
        public string Name { get; set; }

        public object Tag { get; set; }

        public object Clone()
        {
            var model = new ModelVisual3DWithName
            {
                Content = Content.Clone(),
                Name = Name,
                Tag = Tag
            };
            model.SetColor(Brushes.Wheat);
            return model;
        }

        public void SetMaterial(Material material)
        {
            var geometrymodel = Content as GeometryModel3D;
            if (geometrymodel != null)
            {
                geometrymodel.Material = material;
            }
            else
            {

            }
        }

        public Material GetMaterial()
        {
            var geometrymodel = Content as GeometryModel3D;
            if (geometrymodel == null)
            {
                return null;
            }
            return geometrymodel.Material;
        }

        public void SetColor(Brush color)
        {
            var geometrymodel = Content as GeometryModel3D;

            if (geometrymodel.Material is MaterialGroup)
            {
                var materialGroup = geometrymodel.Material as MaterialGroup;
                SetMaterialGroupColor(materialGroup, color);
            }
            else
            {
                DiffuseMaterial material = geometrymodel.Material as DiffuseMaterial;
                if (material != null && !material.IsFrozen)
                {
                    material.Brush = color;
                }
            }
        }


        private void SetMaterialGroupColor(MaterialGroup materialGroup, Brush color)
        {
            foreach (var groupItem in materialGroup.Children)
            {
                if (groupItem is DiffuseMaterial && !groupItem.IsFrozen)
                {
                    var tmpItem = groupItem as DiffuseMaterial;
                    tmpItem.Brush = color;
                }
            }
        }
    }
}
