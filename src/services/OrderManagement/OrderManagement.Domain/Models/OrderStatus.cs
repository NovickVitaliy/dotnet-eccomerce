namespace OrderManagement.Domain.Models;

public static class OrderStatus
{
    public static readonly string[] Statuses = [Created, Processing, Delivering, Delivered];
    public const string Created = "Created";
    public const string Processing = "Processing";
    public const string Delivering = "Delivering";
    public const string Delivered = "Delivered";
}