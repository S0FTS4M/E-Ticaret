package md50013f7905eeb114a2b10c953caf369d6;


public class Person
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ETicaretAndroidAPP.Model.Person, ETicaretAndroidAPP", Person.class, __md_methods);
	}


	public Person ()
	{
		super ();
		if (getClass () == Person.class)
			mono.android.TypeManager.Activate ("ETicaretAndroidAPP.Model.Person, ETicaretAndroidAPP", "", this, new java.lang.Object[] {  });
	}

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
