# DevExpress 13.2 Review - Part 2 #

In the last review, I showed only some of the new small improvements I noticed during migration, so now go to the great stuff!

## WinForms ##

### TaskbarAssistant

The TaskbarAssistent component can help you to integrate the small features of Windows every windows user loves, a seamless and easy to use *Quickstart/Recently used/CustomCommand/Preview/Whatever* -menu for your application.

![](http://i.imgur.com/Z71xAuy.png)
![](http://i.imgur.com/O1cP3Ei.png)

Till now you had to deal with unmanaged code to bring this in your application, or use the [WindowsAPICodePack](http://archive.msdn.microsoft.com/WindowsAPICodePack) but this is very unhandy.

So DevExpress gave us a new API to handle this straight forward, so we can focus on our clients!

#### Use the designer ####

We have a simple Winforms application and start with the designer:

![](http://i.imgur.com/hjhRhyl.png)

Drag it to your form and you got a component:

![](http://i.imgur.com/uCPzjhA.png)

Lets see the options:

![](http://i.imgur.com/a5a8Dm8.png)

As we can see, we have a lot of options to customize our taskbar. 

#### The Jumplist ####

The Jumplist items are basically shortcuts or launch commands to applications. [The Microsoft Documentation](http://msdn.microsoft.com/en-us/library/windows/desktop/dd378460(v=vs.85).aspx#dests) says that tasks are typically `IShellLink` items with command-line arguments.

Okay lets see how we can manage this:

For the default Windows behavior, we should use the JumpListTasksCategory. This will be localized under the different languages of the operating system. For example *Tasks* in english or *Aufgaben* in German.

We now can add custom commands to this like launching the windows calculator or notepad:

![](http://i.imgur.com/sGBxn70.png)
![](http://i.imgur.com/WgIIK1J.png)

Let's launch the application and see what it looks like:

![](http://i.imgur.com/3KUxzCJ.png)

Great! This works like a charm! But let's handle the pictures. Cause this can be even launched if the application is not running (for example the user has pinned your application to the taskbar) we need to add a native image resource.

![](http://i.imgur.com/DFTPV6w.png)
![](http://i.imgur.com/3BhqrxZ.png)

Let's add an icon for both applications:

Grab a [tool](https://www.google.com/search?q=extract+icon+from+exe+&oq=extract+icon+from+exe+&aqs=chrome..69i57j69i59j0l4.4427j0j4&sourceid=chrome&espv=210&es_sm=122&ie=UTF-8) that allows you to extract an icon from an exe or use you own ico file (note it must be in the ICO-Format) (I used the freeware tool [IconExtracts from Nirsoft](http://www.nirsoft.net/))

![](http://i.imgur.com/9K48qyU.png)
![](http://i.imgur.com/JkUHJPo.png)

Close the designers, Rebuild and select your icons:
![](http://i.imgur.com/qxKQBF1.png)

Let's see the result:

![](http://i.imgur.com/Q3EK8Kk.png)

That was easy, without a single line of code we have a full working JumpList. This **is** really amazing!

You also can add a custom JumpList with additional categories, it follows the same scheme but you use the `JumpListCustomCategories`:

![](http://i.imgur.com/xe4taCQ.png)

#### The OverlayIcon ####

Using the overlay icon you can inform your user about the current status of your application. Its a simple Bitmap you can set (Note: I'm using 24x24 PNGs here):

    if (busyCheckEdit.Checked)
        taskbarAssistant1.OverlayIcon = Resources.delete;
    else
        taskbarAssistant1.OverlayIcon = Resources.check;

![](http://i.imgur.com/K2LU3uq.png)

This is a simple API i like, DevExpress thanks for that. No pointer juggling, just a simple, clear and pure managed API.


#### The ThumbnailButtons ####

The last feature i want to talk about are the `ThumbnailButtons`. That are buttons that are visible if the User hovers over your running application in the taskbar:

![](http://i.imgur.com/r8JLKST.png)

![](http://i.imgur.com/eAiESGb.png)

![](http://i.imgur.com/ADix3kn.png)

Now handle the both `Click` events:

![](http://i.imgur.com/tBz69d9.png)

Insert our custom code:

    private void whatsNewTaskbarButton_Click(object sender, ThumbButtonClickEventArgs e)
    {
        webBrowser1.Navigate(@"https://www.devexpress.com/Subscriptions/New-2013.xml");
    }

    private void helpTaskbarButton_Click(object sender, ThumbButtonClickEventArgs e)
    {
        webBrowser1.Navigate(@"http://documentation.devexpress.com/#HomePage/CustomDocument9453");
    }

Start the application and see if everything works as expected.

![](http://i.imgur.com/f3sqaL8.png)

![](http://i.imgur.com/W1BJ1d4.png)

And we are done! In minutes, we implemented full working windows 7 (or greater) taskbar support for our application.

I'm impressed.

### Conclusion ###

Never thought it was so easy to get along with this new control. No need to think about Win32 API, just get it done right. With the first try. I never used this control before, and I'm really impressed how quick i got running with this. Never looked in the documentation (of course the documentation it self is very good)

For me, it is a great proof to rely on the strengths of a manufacturer like DevExpress. When only the best will do.

## What's next? ##

In the next part of the review, i will show you how easy it is to integrate the TaskbarAssistent in an expressAppFramework Winforms project.

Also, i try to show the soft validation feature of XAF and try to cover some other features like adding custom fields and calculated fields at runtime.


Of course, you can follow my progress in my public [GitHub-Repository](https://github.com/biohazard999/DX13_2).

Greetings, Manuel