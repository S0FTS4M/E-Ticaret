package md5a9cad8945bf3787714d7ad485cb8c951;


public class AccountActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("ETicaretAndroidAPP.AccountActivity, ETicaretAndroidAPP", AccountActivity.class, __md_methods);
	}


	public AccountActivity ()
	{
		super ();
		if (getClass () == AccountActivity.class)
			mono.android.TypeManager.Activate ("ETicaretAndroidAPP.AccountActivity, ETicaretAndroidAPP", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
