using System;

// var dimension = new Dimension(10, 10);
var dimension = new Dimension();
Console.WriteLine($"Dimension : {dimension.Height}");


var dimensionInit = new DimensionInit();
// var dimensionInit = new DimensionInit { Width = 10, Height = 10 };
Console.WriteLine($"DimensionInit : {dimensionInit.Height}");


var dimensionClass = new DimensionClass(10, 10);
// var dimensionClass = new DimensionClass();
Console.WriteLine($"DimensionClass : {dimensionClass.Height}");

var dimensionInitClass = new DimensionInitClass();
// var dimensionInitClass = new DimensionInitClass { Width = 10, Height = 10 };
Console.WriteLine($"DimensionInitClass : {dimensionInitClass.Height}");


struct Dimension
{
    public int Width { get; }
    public int Height { get; }

    public Dimension(int width, int height)
    {
        this.Width = width;
        this.Height = height;
    }
}

struct DimensionInit
{
    public int Width { get; init; }
    public int Height { get; init; }
}

class DimensionClass
{
    public int Width { get; }
    public int Height { get; }

    public DimensionClass(int width, int height)
    {
        this.Width = width;
        this.Height = height;
    }
}

class DimensionInitClass
{
    public int Width { get; init; }
    public int Height { get; init; }
}

class BaseClass
{
    public virtual int BaseClassProperty { get; init; }
}

class DerivedClass1 : BaseClass
{
    public override int BaseClassProperty { get; init; }
}

class DerivedClass2 : BaseClass
{
    // Compilation Error: Property must have init to override
    //public override int BaseClassProperty { get; set; } //init }
}