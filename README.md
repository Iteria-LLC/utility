# maths
## How to install
Select "Add package from git URL..." in the Package Manager, and enter this URL:
##### `https://github.com/Iteria-LLC/math.git`

#### Maths Class
Namespaced under `Iteria` by default.
Optionally add scripting define symbol `ITERIA_DENAMESPACE` in player settings to put it in the root namespace for easier access.

#### Square Distance
Use static function `Square.Distance(a, b)` to perform distance comparisons without calculating square roots.
```csharp
if(Square.Distance(transform.position, point) < 5f)
    Debug.Log("Within 5 meters!");
```
