There are two reasons not to use the default component properties (e.g. `rigidBody`, `particleSystem`, etc) on `MonoBehaviour`:

1. The properties do no caching, so each time you use the property you are calling `GetComponent`.
2. Unity 5 is [removing all of them](http://blogs.unity3d.com/2014/06/23/unity5-api-changes-automatic-script-updating/) aside from the `transform` property.

`CacheBehaviour` is a drop-in replacement for `MonoBehaviour` that exposes all the same properties as `MonoBehavior`. The properties will check the cache variable and call `GetComponent` anytime the cached value is invalid.

Note
===

There used to be a `FastCacheBehaviour` that used fields and various methods to obtain the values, trying to get better performance. While this did work, in my experience with my current title, this was a giant pain and in cases where you need direct field access you're better off just doing that work yourself. Then you'll have far fewer fields and you won't be calling strange methods. `CacheBehaviour` is still a nice addition in light of 5.0 and for caching things in 4.x today.
