# Документация Character ([eng](Character_en.md))

Этот документ описывает ключевые игровые сущности: Character, Player, Enemy и их взаимодействие. В документе рассмотрены их инсталлеры и способы интеграции зависимостей.

![Схема взаимодействия контекстов](Images/UsingCharacterContext.png)

В проекте используются основные игровые сущности: **Character**, **Player**, **Enemy**, **Weapon** и **UI-система**. Они взаимодействуют через систему контекстов, управляемую **Zenject**, что позволяет гибко настраивать зависимости и облегчает расширение функционала. **GameObject Context** помогает организовать связи между персонажами, оружием и интерфейсными элементами, обеспечивая модульность и удобство интеграции новых механик.

## Главные префабы

- **Character** – базовый игровой персонаж.
- **Enemy** – префаб противника, использует **Character**.
- **Player** – префаб игрока, использует **Character**.
- **HealthBar**, **ReloadBar**, **LookDirectionDisplay** – UI-компоненты.
- **Weapon** – префаб оружия.
- **EnemySpawner**, **OnceEnemySpawner**, **RepeatableEnemySpawner** – спавнеры врагов.

## Инсталлеры

Zenject-инсталлеры используются для настройки зависимостей и управления контекстами. Подробнее о работе инсталлеров можно ознакомиться в [официальной документации Zenject](https://github.com/modesttree/Zenject?tab=readme-ov-file#installers).

### **CharacterInstaller**

- Связывает зависимости для **Character**.
- Внедряет **ICharacterSettings**, **IFriendOrFoeTag**, **IDamageCalculator**, **IWeaponOwner**.
- Подключает UI-элементы и эффекты.

### **EnemyInstaller**

- Внедряет **EnemySettings** и **IAIBehaviourInstaller**.
- Создаёт **Character** через сабконтейнер.

### **PlayerInstaller**

- Внедряет **IPlayerSettings** и **IFriendOrFoeTag**.
- Добавляет **Character** и подключает ввод через **PlayerInputAdapter**.

### **PlayerControllerInstaller**

- Внедряет **PlayerInputAdapter**, **IMovable**, **MonoTicker**.
- Связывает ввод с передвижением и атакой игрока.

## Метасущности

В проекте реализована система префаб-вариантов, которая позволяет переиспользовать код и настройки для различных игровых сущностей. Каждый префаб несёт на себе дополнительную логику, адаптируя базовый функционал **Character** под конкретные игровые нужды, такие как управление игроком или поведение врагов. Благодаря **GameObject Context** и внедрению зависимостей через **Zenject**, скрипты развязаны между собой и получают необходимые зависимости через контейнер, что обеспечивает гибкость и удобство расширения.

### **Character**

- Используется как основа для **Player** и **Enemy**.
- Связан с **ICharacterSettings**, **IWeaponOwner**, **IHaveHealth**.
- Управляет здоровьем, атакой, движением и взаимодействием с оружием.

### **Enemy**

- Использует **Character**.
- Не имеет собственного класса, а работает через **Character** с настройками для противников.
- Получает параметры из **EnemySettings**.
- Настраивается через **EnemyInstaller**.

### **Player**

- Использует **Character**.
- Управляется через **PlayerInputAdapter**.
- Настраивается через **PlayerInstaller**.

## Подсистемы

### **Боевая система**

- **IWeapon**, **IWeaponOwner**, **ICanFire**, **ICanReload** – механика оружия.
- **IDamageCalculator** – расчёт урона.

### **Система передвижения**

- **IMovable** – интерфейс для управления движением.
- **MoveBehaviour** – реализация передвижения через **Rigidbody**.

### **AI-система**

Система искусственного интеллекта отвечает за поведение противников и их взаимодействие с окружением. Она реализуется через **IAIBehaviourInstaller**, который устанавливает и управляет различными AI-стратегиями.

#### **Основные компоненты**

- **IAIBehaviourInstaller** – интерфейс для установки AI-логики.
- **EnemySpawner** – создаёт противников с заданными настройками.
- **RepeatableEnemySpawner** – спавнит врагов с задержкой и ограничением по количеству.
- **AI State Machine** – система состояний, управляющая поведением врага (например, патрулирование, атака, преследование).

#### **Принципы работы**

1. **Враг создаётся через EnemySpawner**, получая зависимости через Zenject.
2. **IAIBehaviourInstaller** настраивает логику передвижения и атаки.
3. **AI State Machine** переключает состояния врага в зависимости от игровых событий.
4. **RepeatableEnemySpawner** может порождать новых врагов, если их число падает ниже порога.

Эта модульная структура позволяет легко расширять и модифицировать поведение противников без переписывания кода базового AI.

### **UI-система**

- **HealthBar**, **ReloadBar**, **LookDirectionDisplay** – элементы интерфейса.
- **CharacterColorFeedback** – визуальные эффекты повреждений.

### **Система ввода**

- **IInputController** – управление игроком.
- **PlayerInputAdapter** – связывает ввод с действиями персонажа.

### **Система дружбы/вражды**

- **FriendOrFoeComponent** – определяет союзников и врагов.
- **IFriendOrFoeTag** – обозначает принадлежность персонажа к команде.

---

## Взаимодействие между сущностями

| Сущность  | Взаимодействует с        | Основные зависимости                          |
| --------- | ------------------------ | --------------------------------------------- |
| Character | Player, Enemy            | ICharacterSettings, IWeaponOwner, IHaveHealth |
| Player    | Character                | PlayerInputAdapter, IPlayerSettings           |
| Enemy     | Character, AI-система    | EnemySettings, IAIBehaviourInstaller          |
| Weapon    | Character, Player, Enemy | IWeaponOwner, ICanFire, ICanReload            |
| Spawners  | EnemyFactory             | EnemySettings                                 |
| UI        | Character, Player        | HealthBar, ReloadBar, LookDirectionDisplay    |

