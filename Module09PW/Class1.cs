using System;
using System.Linq;

abstract class Storage
{
    private string name;
    private string model;

    public Storage(string name, string model)
    {
        this.name = name;
        this.model = model;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Model
    {
        get { return model; }
        set { model = value; }
    }

    public abstract double GetStorageVolume();

    public abstract void CopyData(double dataSize);

    public abstract double GetFreeSpace();

    public abstract string GetDeviceInfo();
}

class Flash : Storage
{
    private double usbSpeed;
    private double memorySize;

    public Flash(string name, string model, double usbSpeed, double memorySize)
        : base(name, model)
    {
        this.usbSpeed = usbSpeed;
        this.memorySize = memorySize;
    }

    public override double GetStorageVolume()
    {
        return memorySize;
    }

    public override void CopyData(double dataSize)
    {
        Console.WriteLine($"Copying data to Flash. Speed: {usbSpeed} MB/s. Data size: {dataSize} MB");
    }

    public override double GetFreeSpace()
    {
        return memorySize - GetStorageVolume();
    }

    public override string GetDeviceInfo()
    {
        return $"Flash Drive: {Name}, Model: {Model}, USB Speed: {usbSpeed} MB/s, Memory Size: {memorySize} MB";
    }
}

class DVD : Storage
{
    private double readWriteSpeed;
    private string type;

    public DVD(string name, string model, double readWriteSpeed, string type)
        : base(name, model)
    {
        this.readWriteSpeed = readWriteSpeed;
        this.type = type;
    }

    public override double GetStorageVolume()
    {
        return type == "Single-layer" ? 4.7 : 9;
    }

    public override void CopyData(double dataSize)
    {
        Console.WriteLine($"Copying data to DVD. Speed: {readWriteSpeed} MB/s. Data size: {dataSize} MB");
    }

    public override double GetFreeSpace()
    {
        return GetStorageVolume();
    }

    public override string GetDeviceInfo()
    {
        return $"DVD: {Name}, Model: {Model}, Read/Write Speed: {readWriteSpeed} MB/s, Type: {type}";
    }
}

class HDD : Storage
{
    private double usbSpeed;
    private int partitions;
    private double partitionSize;

    public HDD(string name, string model, double usbSpeed, int partitions, double partitionSize)
        : base(name, model)
    {
        this.usbSpeed = usbSpeed;
        this.partitions = partitions;
        this.partitionSize = partitionSize;
    }

    public override double GetStorageVolume()
    {
        return partitions * partitionSize;
    }

    public override void CopyData(double dataSize)
    {
        Console.WriteLine($"Copying data to HDD. Speed: {usbSpeed} MB/s. Data size: {dataSize} MB");
    }

    public override double GetFreeSpace()
    {
        return partitions * partitionSize - GetStorageVolume();
    }

    public override string GetDeviceInfo()
    {
        return $"HDD: {Name}, Model: {Model}, USB Speed: {usbSpeed} MB/s, Partitions: {partitions}, Partition Size: {partitionSize} MB";
    }
}

class BackupApplication
{
    static void Main(string[] args)
    {
        Storage[] devices = new Storage[]
        {
            new Flash("Flash1", "Model1", 100, 2048),
            new DVD("DVD1", "Model2", 10, "Single-layer"),
            new HDD("HDD1", "Model3", 50, 2, 1024)
        };

        foreach (var device in devices)
        {
            Console.WriteLine(device.GetDeviceInfo());
            Console.WriteLine($"Free space: {device.GetFreeSpace()} MB");
            Console.WriteLine($"Copying data...");
            device.CopyData(565);
            Console.WriteLine();
        }

        double totalMemory = devices.Sum(d => d.GetStorageVolume());
        Console.WriteLine($"Total storage volume: {totalMemory} MB");
    }
}
