
namespace Store.Entities
{
    public enum StatusOfOrder
    {
        New,
        CanceledByAdministrator,
        PaymentReceived,
        Sent,
        Received,
        Completed,
        CanceledByUser,
    }
}
