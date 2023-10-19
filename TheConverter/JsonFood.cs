using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TheConverter;

internal class JsonFood
{
    public string Name { get; set; }
    public string Producer { get; set; }
    public float UnitPrice { get; set; }
    public float PriceKg { get; set; }
    public float? UnitAmount {  get; set; }
    public JsonElement Categories { get; set; }
    public Macro Macro { get; set; }
    public Ref Ref { get; set; }
}

internal class Macro
{
    public float Calories { get; set; }
    public float Fat {  get; set; }
    public float Carbohydrates { get; set; }
    public float Protein { get; set; }
}

internal class Ref
{
    public string Name { get; set; }
}