# Code Architecture

The game's code architecture adopts a dual-concept approach: **Model and Node**.

### Model vs Node

- **Node**: Integrates a mechanic, being aware of the engine and in-game systems. Derives from Godot Node or its subclasses.
  
- **Model**: Abstracts the mechanic's logic. It's engine-agnostic, making unit testing easier, and even allows potential migration to another engine. (I'm looking at you Unity 🤨) 

#### Observations
  - A **Model** only manages logic. Nodes, on the other hand, can possess a model to fetch values or alter states.
  - This design is inspired by the **Humble Object Pattern**.
  - Complex models dependent on external libraries (that are proven to be to difficult to unit test) may transform into a **System** instead.

### Core Models & Nodes

- **GameModel/Node**: Reflects the game's overall functionality. It's persistent across scenes (Singleton).
  Housed within this scope: https://github.com/KaitoMajima/WhirlyBird/blob/main/%24Game/Scripts/Core/Game/GameScope.cs
  
- **LoadingSystem/Node**: Manages the game loading system, it's persistent across scenes (Singleton).
  Housed within this scope: https://github.com/KaitoMajima/WhirlyBird/blob/main/%24Game/Scripts/Loading/LoadingScope.cs

- **MainMenuModel/Node**: Specific to the main menu and is removed once a new scene loads.
  Housed within this scope: https://github.com/KaitoMajima/WhirlyBird/blob/main/%24Game/Scripts/Core/MainMenu/MainMenuScope.cs

- **MapModel/Node**: Specific to the active game map and is removed once a new scene loads.
  Housed within this scope: https://github.com/KaitoMajima/WhirlyBird/blob/main/%24Game/Scripts/Core/Map/MapScope.cs

### Architectural Scope

Each primary model and node is housed within a designated Scope, such as **GameScope** or **MapScope**. These scopes take charge of dependency management via injection, with models and nodes being instantiated through Factories. 

Sub-models and nodes are nested within these core models and nodes, offering a robust foundation for an [IoC (Inversion of Control)](https://en.wikipedia.org/wiki/Inversion_of_control) dependency-injected system.

### Unit Tests
All models are tested individually with NUnit, you can find the tests within the $Tests folder in the root of this project. Any model's dependencies are mocked using NSubstitute, allowing to test most case scenarios.
