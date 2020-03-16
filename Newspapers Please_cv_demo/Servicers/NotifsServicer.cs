using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

namespace Global
{
    public static class NotifsServicer
    {
        const string CHANNEL_ID = "newspapersplease_notifications";

        public static void Init()
        {
            var channel = new AndroidNotificationChannel()
            {
                Id = CHANNEL_ID,
                Name = "Newspapers, Please!",
                Description = "Newspapers, Please! Notifications channel.",
                Importance = Importance.Default
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
            Debug.Log("Notification Channel Initialized");
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        static bool IsNotifShiftExists => Modeler.Game.notifShiftId != -1;

        public static void CreateNotifShift()
        {
            if (!Modeler.Game.areNotifsOn)
                return;

            var nextShiftTime = System.DateTime.FromBinary(Modeler.Week.waitTimeBinary);
            AddNotification(nextShiftTime, false);

            Debug.Log("Notification is created");
        }

        public static void RemoveNotifShift()
        {
            if (!IsNotifShiftExists)
                return;

            AndroidNotificationCenter.CancelNotification(Modeler.Game.notifShiftId);
            Modeler.Game.notifShiftId = -1;

            Debug.Log("Notification is removed");
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        static bool AreNotifsRemindersExist => Modeler.Game.notifsRemindersIds.Count > 0;

        public static void CreateNotifsReminders()
        {
            if (!Modeler.Game.areNotifsOn)
                return;

            var fireTime = System.DateTime.Now;
            for (var i = 0; i < 5; i++)
            {
                fireTime = fireTime.AddDays(1);
                AddNotification(fireTime, true);
            }
            Modeler.SaveGameSettings();

            Debug.Log("New reminders are set");
        }

        public static void RemoveNotifsReminders()
        {
            if (!AreNotifsRemindersExist)
                return;

            foreach (var notifId in Modeler.Game.notifsRemindersIds)
                AndroidNotificationCenter.CancelNotification(notifId);
            Modeler.Game.notifsRemindersIds.Clear();
            Modeler.SaveGameSettings();

            Debug.Log("Reminders are removed");
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        static void AddNotification(System.DateTime fireTime, bool isReminder)
        {
            var notif = new AndroidNotification()
            {
                Title = Stringer.NotificationTitle,
                Text = Stringer.NotificationText,
                LargeIcon = "large_icon",
                FireTime = fireTime
            };
            var notifId = AndroidNotificationCenter.SendNotification(notif, CHANNEL_ID);

            if (Application.isEditor)
                notifId = 0;

            if (isReminder)
                Modeler.Game.notifsRemindersIds.Add(notifId);
            else
                Modeler.Game.notifShiftId = notifId;
        }
    }
}
