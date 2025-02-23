---
title: "Документация Character"
---

# Документация Character

## Главные префабы
- **Character** – базовый игровой персонаж.
- **Enemy** – префаб противника, использует **Character**.
- **Player** – префаб игрока, использует **Character**.
- **HealthBar**, **ReloadBar**, **LookDirectionDisplay** – UI-компоненты.
- **Weapon** – префаб оружия.
- **EnemySpawner**, **OnceEnemySpawner**, **RepeatableEnemySpawner** – спавнеры врагов.

## Инсталлеры

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

### **Character**
- Используется как основа для **Player** и **Enemy**.
- Связан с **ICharacterSettings**, **IWeaponOwner**, **IHaveHealth**.
- Управляет здоровьем, атакой, движением и взаимодействием с оружием.

### **Enemy**
- Использует **Character**.
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
- **IAIBehaviourInstaller** – установка логики противников.
- **EnemySpawner**, **RepeatableEnemySpawner** – спавн противников.

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

| Сущность     | Взаимодействует с        | Основные зависимости |
|-------------|----------------------|-------------------|
| Character   | Player, Enemy        | ICharacterSettings, IWeaponOwner, IHaveHealth |
| Player      | Character            | PlayerInputAdapter, IPlayerSettings |
| Enemy       | Character, AI-система | EnemySettings, IAIBehaviourInstaller |
| Weapon      | Character, Player, Enemy | IWeaponOwner, ICanFire, ICanReload |
| Spawners    | EnemyFactory         | EnemySettings |
| UI          | Character, Player    | HealthBar, ReloadBar, LookDirectionDisplay |

