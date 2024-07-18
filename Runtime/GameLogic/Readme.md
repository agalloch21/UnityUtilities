## Description
Only implement core functionalities here, for more advanced functionalities, use Gamelogic extensions instead.

Implemented functionalities:
- **GenericSingleton\<T>**

---
## Gamelogic Extensions by ©Gamelogic
- Asset Store: https://assetstore.unity.com/packages/tools/utilities/gamelogic-extensions-19323



### Pattern Classes

- **Singleton**: The classic Unity singleton for providing global access to certain MonoBehaviours.
Pools: Classes for reusing objects, with optimizations for hashable objects. Designed to build custom pools from.
- **StateMachine**: Class for managing states and methods that should execute when states change or as it persists. Designed to build custom state machines from.
PushdownAutomaton: A state machine that remembers past states and can transition back to them FIFO-style.
- **Clock**: Class for executing once-of or regularly timed events. Includes an event for seconds to make clock UIs easier to implement.
- **ImplementationFactory**: Class for creating objects from generic parameters, especially useful for testing different implementations of the same interface. Can serve as the basis for a service locator.
- **ValueSnapshot**: Class for representing a value and previous value. 
- **ObservedValue**: Class for raising events when a value changes.
- **Optional**: Types to indicate optional values in the inspector.
- **StateTracker**: Classes for raising events when sets of operations complete.

### Data Structures and Algorithms

- **Collection extensions**: Provides useful extension methods for collections, lists, enumerables, and arrays.
- **RingBuffer**: A ring buffer implementation. 
- **Generators**: Objects that work like random generators, but not random. Useful for creating patterns, or random values with elaborate constraints.
- **ResponseCurve**: Represents a piecewise linear function from float to any “continuous” type. Ships with response curves for float, Vector2, Vector3, Vector4, and Color.
PIDController, Differentiator, Integrator: classes for doing PID control. 
- **Combinatorial algorithms**: Class for generating combinations, permutations, the power set, and partitions of a set.
- **L-System**: Supports rewriting strings using rules that can be the basis of certain procedural generation algorithms.

### Extensions

- **PlayerPrefs**: Adds support for bool and array types, and for scoping.
MonoBehaviour: Provides convenience methods such as Instantiate, Invoke (using actions), Tween, GetRequiredComponent, DestroyUniversal, and Clone.
- **GameObject**: Provides GetRequiredComponent.
Transform: Provides convenience methods for common transformations, useful lazy enumerables.
- **Vector**: Provides methods to get a copy of a vector with some coordinates changed, conversion methods from 2D to 3D, methods for basic transformations, “projection and rejection operations, the perp dot product, and Hadamard multiplication and division.
- **Color**: Provides convenient methods to get a color lighter, darker, or with specified brightness or alpha value.
- **Math**: Provides division and modular operations that work correctly with negative numbers, methods for getting fractional value or sign, and methods for doing circular lerping.
- **Debug**: Provides a way to add a label to messages, useful when working with console extensions such as Console Pro.
- **Image**: Adds SetAlpha and Set Visible methods.

### Editor Extensions

- **Useful decorators and property drawers**: Comment, Dummy, Highlight, Flags, Labels, NonNegative, Positive, and WarningIfNull.
- **FieldTypes**: List (more usable than older Unity lists), MinMaxFloat, and MinMaxInt.
- **Inspector buttons**: Turn methods into buttons that display at the bottom of a component.
- **GLEditorGUI**: Convenient base class for editors.

### Utilities

- **ScreenshotTaker**: Capture screenshots in both the game and the editor effortlessly.