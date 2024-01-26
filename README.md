# utility
## How to install
Select "Add package from git URL..." in the Package Manager, and enter this URL:
```
https://github.com/Iteria-LLC/utility.git
```

#### Extension Methods
Collection indexing helpers, for arrays, lists, and dictionaries.

`void IncrementIndex<T>(this ref int index, T[] array, bool wrap = true)`
`void DecrementIndex<T>(this ref int index, T[] array, bool wrap = true)`
Returns index, wrapped to the bounds of the given collection.
```csharp
void NextWeapon()
{
    //Scroll through weapons, wrapping back to 0 if necessary.
    weaponIndex.IncrementIndex(weaponArray);
}
```

#### Maths Class
Namespaced under `Iteria` by default.
Optionally add scripting define symbol `ITERIA_DENAMESPACE` in player settings to put it in the root namespace for easier access.

#### Square Distance
Use static function `Square.Distance(a, b)` to perform distance comparisons without calculating square roots.
```csharp
if(Square.Distance(transform.position, point) < 5f)
    Debug.Log("Within 5 meters!");
```
