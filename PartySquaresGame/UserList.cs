using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace User_NS
{
   /// <summary>
   /// Selects the tile based on the current score
   /// </summary>
   public class UserList
   {
      /// <summary>
      /// Constructor
      /// </summary>
      public UserList()
      {
         ListUsers = new Dictionary<string, User>();
         UserListBox = new ListView();
      }

      /// <summary>
      /// Constructor with list box parameter.
      /// </summary>
      /// <param name="listBox"></param>
      public UserList(ListView listBox)
      {
         SetListBox(listBox);
      }

      /// <summary>
      /// Set the User List Box
      /// </summary>
      /// <param name="listBox"></param>
      public void SetListBox(ListView listBox)
      {
         UserListBox = listBox;
      }

      /// <summary>
      /// Insert user into the user list.
      /// </summary>
      /// <param name="user"></param>
      public void InsertUser(User user)
      {
         if (user != null)
         {
            ListUsers[user.Name] = user;
         }

         SortList();
      }

      /// <summary>
      /// Insert user into the user list using 2 parameters.
      /// </summary>
      /// <param name="name"></param>
      /// <param name="user"></param>
      public void InsertUser(string name, User user)
      {
         if (user != null)
         {
            ListUsers[name] = user;
         }

         SortList();
      }

      /// <summary>
      /// Returns true if a user's name is
      /// in the user list. 
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      private bool KeyExists(string name)
      {
         bool keyFound = false;
         foreach(KeyValuePair<string, User> key in ListUsers)
         {
            if(key.Key == name)
            {
               keyFound = true;
               break;
            }
         }
         return keyFound;
      }

      /// <summary>
      /// Returns a user with the given name.
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public User GetUser(string name)
      {
         if (KeyExists(name))
         {
            return ListUsers[name];
         }
         else
         {
            return null;
         }
      }

      /// <summary>
      /// Update the user object in the user list.
      /// </summary>
      /// <param name="user"></param>
      public void UpdateUser(User user)
      {
         if(user != null)
         {
            string userName = user.Name;
            ListUsers[userName] = user;

            SortList();
         }
      }

      /// <summary>
      /// Remove a user from the user list. 
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public bool RemoveUser(string name)
      {
         UserListBox.Items.RemoveByKey(name);
         return ListUsers.Remove(name);
      }

      /// <summary>
      /// Sort the user list.
      /// </summary>
      public void SortList()
      {
         Dictionary<string, User> newList = new Dictionary<string, User>();
         while(ListUsers.Count > 0)
         {
            User maxUser = new User();
            foreach (User user in ListUsers.Values)
            {
               if(user.MoneyAmount.Value >= maxUser.MoneyAmount.Value)
               {
                  maxUser = user;
               }
            }
            newList[maxUser.Name] = maxUser;
            ListUsers.Remove(maxUser.Name);
         }
         ListUsers = newList;

         RefreshListBox();
      }

      /// <summary>
      /// Refresh the List Box control
      /// </summary>
      public void RefreshListBox()
      {
         UserListBox.Items.Clear();

         UserListBox.BeginUpdate();
         foreach(KeyValuePair<string, User> userPair in ListUsers)
         {
            ListViewItem listItem = new ListViewItem();
            listItem.Text = userPair.Value.ToString();
            listItem.BackColor = userPair.Value.Color;
            UserListBox.Items.Add(listItem);
         }
         UserListBox.EndUpdate();
      }

      /// <summary>
      /// Perform a clear operation on the user list and
      /// the list box.
      /// </summary>
      public void Clear()
      {
         ListUsers.Clear();
         UserListBox.Items.Clear();
      }

      /// <summary>
      /// Return the number of users.
      /// </summary>
      public int Count
      {
         get { return ListUsers.Count; }
      }

      public Dictionary<string, User> ListUsers
      {
         get;
         private set;
      }

      public ListView UserListBox
      {
         get;
         private set;
      }
   }
}
