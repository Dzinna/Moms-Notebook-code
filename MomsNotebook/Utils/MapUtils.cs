using MomsNotebook.Models.Database;

namespace MomsNotebook.Utils
{
    public static class MapUtils
    {
        public static void MapContacts(this Contacts contacts, Contacts contactsFromForm)
        {
            contacts.Key = contactsFromForm.Key;
            contacts.Address = contactsFromForm.Address;
            contacts.FlatNumber = contactsFromForm.FlatNumber;
            contacts.HouseNumber = contactsFromForm.HouseNumber;
            contacts.Mailbox = contactsFromForm.Mailbox;
            contacts.ContactName = contactsFromForm.ContactName;
            contacts.Telephone = contactsFromForm.Telephone;
        }

        public static void MapFeedings(this Feeding feeding, Feeding feedingFromForm)
        {
            feeding.Key = feedingFromForm.Key;
            feeding.ActualTime = feedingFromForm.ActualTime;
            feeding.FoodDescription = feedingFromForm.FoodDescription;
            feeding.FoodType = feedingFromForm.FoodType;
            feeding.Quantity = feedingFromForm.Quantity;
        }
    }
}
