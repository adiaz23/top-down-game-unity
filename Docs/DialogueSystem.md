# 🎮 Documentación del Sistema de Diálogos

## Tabla de Contenidos
1. [Resumen General](#resumen-general)
2. [Configuración Inicial](#configuración-inicial)
3. [Creando Diálogos](#creando-diálogos)
4. [Configurando Personajes](#configurando-personajes)
5. [Probando tu Diálogo](#probando-tu-diálogo)
6. [Características Avanzadas](#características-avanzadas)
7. [Solución de Problemas](#solución-de-problemas)

---

## Resumen General

Este sistema de diálogos te permite crear conversaciones entre el jugador y NPCs/enemigos con:
- ✅ **Diálogos ramificados** (opciones del jugador)
- ✅ **Efecto de máquina de escribir**
- ✅ **Información de personaje automática** (nombres/retratos por defecto)
- ✅ **Múltiples tipos de personajes** (NPCs, Enemigos)
- ✅ **Listo para integración de misiones**

### Estructura de Archivos
```
Assets/Scripts/
├── Dialogue/
│   ├── DialogueData.cs
│   ├── DialogueSystem.cs
│   └── DialogueTrigger.cs
└── Characters/
    ├── Character.cs
    ├── NPC.cs
    └── Enemy.cs
```

---

## Configuración Inicial

### 1. Configuración de UI (Solo Una Vez)

Tu Canvas debe tener esta estructura:
```
Canvas 
├── Dialogue Panel
    ├── ID Panel
    │   ├── Image (Retrato del Hablante)
    │   └── Text name (Nombre del Hablante - TextMeshPro)
    └── Dialogue Text Panel
        ├── Dialogue Text (TextMeshPro)
        ├── Continue Button TMP (Botón)
        └── ChoicesContainer (GameObject Vacío)
```

### 2. Crear Prefab de Botón de Opciones (Solo Una Vez)

1. **Crear un UI Button** en tu escena
2. **Añadir TextMeshPro** como hijo para el texto del botón
3. **Darle estilo** (colores, tamaño, fuente)
4. **Arrastrarlo a la carpeta Project** para crear el prefab
5. **Eliminarlo de la escena**

### 3. Configurar DialogueSystem (Solo Una Vez)

1. **Crear GameObject vacío** llamado "DialogueSystem"
2. **Añadir script DialogueSystem**
3. **Asignar todas las referencias de UI:**

| Campo | Asignar A |
|-------|-----------|
| Dialogue Panel | Tu GameObject Dialogue Panel |
| Dialogue Text | Tu Dialogue Text (TextMeshPro) |
| Speaker Name Text | Tu Text name (TextMeshPro) |
| Speaker Icon | Tu componente Image |
| Continue Button | Tu Continue Button |
| Choices Parent | Tu ChoicesContainer |
| Choice Button Prefab | Tu prefab de botón del Project |

4. **Configurar Layout** en ChoicesContainer:
   - Añadir **Vertical Layout Group**
   - ✓ Control Child Size (Width & Height)
   - **Spacing:** 10
   - **Padding:** 10

---

## Creando Diálogos

### Paso 1: Crear Asset DialogueData

1. **Click derecho en Project** → Create → RPG → Dialogue Data
2. **Nómbralo** descriptivamente (ej: "TenderoSaludo", "DadorMisiones_Intro")

### Paso 2: Configurar Información Básica

```
Character Name: "Tendero Bob"     (opcional - usará el fallback de Character class)
Character Icon: [Imagen Retrato] (opcional - usará el fallback de Character class)
```

### Paso 3: Crear Entradas de Diálogo

Cada entrada representa una "burbuja de diálogo" en la conversación.

#### Diálogo Lineal Simple:
```
Entry 0:
├── Text: "¡Bienvenido a mi tienda!"
├── Speaker Name: [dejar vacío para usar el por defecto]
├── Speaker Icon: [dejar vacío para usar el por defecto]
├── Is Last Entry: ✓ (si esto termina la conversación)
└── Choices: [vacío para diálogo lineal]
```

#### Diálogo Ramificado con Opciones:
```
Entry 0:
├── Text: "¿En qué puedo ayudarte?"
├── Is Last Entry: □ (desmarcado)
└── Choices:
    ├── Choice 0: "Quiero comprar" → Next Index: 1
    ├── Choice 1: "Quiero vender" → Next Index: 2
    └── Choice 2: "Adiós" → Next Index: -1

Entry 1:
├── Text: "¡Aquí están mis productos!"
├── Is Last Entry: ✓
└── Choices: [vacío]

Entry 2:
├── Text: "¡Muéstrame lo que tienes!"
├── Is Last Entry: ✓
└── Choices: [vacío]
```

### Paso 4: Reglas de Flujo de Diálogo

| Valor Next Index | Qué Sucede |
|------------------|------------|
| **-1** | Terminar conversación |
| **0, 1, 2, etc.** | Saltar a esa entrada de diálogo |
| **Mismo número** | Quedarse en la entrada actual (bucle infinito) |

---

## Configurando Personajes

### Para NPCs

1. **Seleccionar tu GameObject NPC**
2. **Add Component:** Script NPC
3. **Configurar Información del Personaje:**
   ```
   Character Name: "Anciano del Pueblo"    (nombre por defecto)
   Character Sprite: [Retrato Anciano]     (imagen por defecto)
   Dialogues: [Arrastra tus assets DialogueData aquí]
   Current Dialogue Index: 0
   ```

4. **Añadir/Configurar DialogueTrigger:**
   ```
   Dialogue Data: [Tu DialogueData principal]
   Trigger On Collision: ✓
   Interaction Key: E
   Interaction Range: 2
   ```

5. **Añadir Collider:**
   - **BoxCollider2D** con **Is Trigger** ✓

### Para Enemigos

1. **Add Component:** Script Enemy
2. **Configurar:**
   ```
   Character Name: "Guerrero Goblin"
   Character Sprite: [Retrato Goblin]
   Battle Start Dialogue: [DialogueData de Provocación]
   Defeat Dialogue: [DialogueData de Derrota]
   ```

### Configuración del Jugador

**Asegúrate de que tu GameObject Player tenga el tag "Player"**

---

## Probando tu Diálogo

### Lista de Verificación Rápida

1. **Reproduce la escena**
2. **Camina al área de trigger del NPC**
3. **Presiona E** para iniciar el diálogo
4. **Verifica que funcione:**
   - ✅ Aparece el panel de diálogo
   - ✅ Se muestra el nombre del personaje (fallback o especificado)
   - ✅ Se muestra el retrato del personaje (fallback o especificado)
   - ✅ Aparece el texto (con efecto máquina de escribir)
   - ✅ Funciona el botón continuar
   - ✅ Aparecen las opciones cuando se especifican
   - ✅ Hacer clic en opciones avanza el diálogo
   - ✅ El diálogo termina correctamente

### Herramientas de Debug

Añade estos para ayudar en la solución de problemas:

**En DialogueTrigger.cs OnTriggerEnter2D:**
```csharp
Debug.Log($"El jugador entró al área de trigger de {gameObject.name}");
```

**En DialogueSystem.cs StartDialogue:**
```csharp
Debug.Log($"Iniciando diálogo: {dialogue.name} con personaje: {character?.characterName}");
```

---

## Características Avanzadas

### Múltiples Diálogos por NPC

Los NPCs pueden tener diferentes conversaciones basadas en el estado del juego:

```csharp
// En código, cambiar qué diálogo está activo
npc.SetDialogueIndex(1);  // Cambiar al segundo diálogo
npc.NextDialogue();       // Ir al siguiente diálogo en el array
```

Casos de uso de ejemplo:
- **Index 0:** Primer encuentro
- **Index 1:** Después de dar la misión
- **Index 2:** Después de completar la misión

### Integración de Misiones

```csharp
// En tu sistema de misiones
public void OnQuestComplete()
{
    NPC questGiver = FindObjectOfType<NPC>();
    questGiver.CompleteQuest();  // Cambia al diálogo de completación
}
```

### Nombres de Hablante Personalizados por Entrada

Puedes sobrescribir el nombre por defecto del personaje para entradas específicas:

```
Entry 0:
├── Speaker Name: [vacío] → Usa "Anciano del Pueblo"
├── Text: "¡Hola!"

Entry 1:
├── Speaker Name: "Anciano Misterioso" → Sobrescribe el por defecto
├── Text: "Tengo un secreto que contarte..."
```

### Diálogos en Bucle

Crear un diálogo de "menú principal" que regrese a sí mismo:

```
Entry 0: "¿Qué necesitas?" (Menú Principal)
├── "Tienda" → Index 1
├── "Misiones" → Index 2
└── "Adiós" → Index -1

Entry 1: "¡Aquí está mi tienda!"
└── "Volver" → Index 0  (regresa al menú principal)

Entry 2: "Tengo estas misiones:"
└── "Volver" → Index 0  (regresa al menú principal)
```

### Atajos de Teclado

- **Enter/Espacio:** Continuar diálogo (cuando el botón continuar es visible)
- **E:** Interactuar con NPCs (configurable en DialogueTrigger)

---

## Solución de Problemas

### Problemas Comunes

#### "El diálogo no inicia"
- ✅ Verificar que Player tenga el tag "Player"
- ✅ Verificar que NPC tenga trigger collider
- ✅ Verificar que DialogueSystem existe en la escena
- ✅ Verificar que DialogueData está asignado

#### "No se muestra el nombre/imagen del personaje"
- ✅ Verificar que el script Character tenga characterName/characterSprite configurados
- ✅ Verificar que las referencias UI de DialogueSystem están asignadas
- ✅ Dejar vacíos los campos de personaje en DialogueData para usar fallbacks

#### "Los botones de opciones no aparecen"
- ✅ Verificar que ChoicesContainer tenga Vertical Layout Group
- ✅ Verificar que Choice Button Prefab tenga componente TextMeshPro
- ✅ Verificar que las opciones tengan valores Next Index apropiados

#### "Los botones se superponen o se ven mal"
- ✅ Añadir Content Size Fitter a ChoicesContainer
- ✅ Configurar Vertical Fit a "Preferred Size"
- ✅ Ajustar spacing/padding del Layout Group

#### "Errores en consola"
- ✅ Verificar que todas las referencias UI en DialogueSystem están asignadas
- ✅ Verificar que DialogueData tiene al menos una entrada
- ✅ Verificar que la estructura del Choice Button Prefab es correcta

### Comandos de Debug en Consola

Usa estos para pruebas:

```csharp
// Iniciar diálogo manualmente
DialogueSystem.Instance.StartDialogue(myDialogueData, myCharacter);

// Verificar si el sistema existe
Debug.Log(DialogueSystem.Instance != null);

// Probar componente character
Debug.Log(GetComponent<Character>() != null);
```

---

## Referencia Rápida

### Componentes Esenciales para NPCs:
- ✅ Script NPC (o script Enemy)
- ✅ Script DialogueTrigger
- ✅ BoxCollider2D (Is Trigger ✓)
- ✅ Asset(s) DialogueData

### Flujo de Trabajo para Creación de Archivos:
1. **Crear DialogueData** → Configurar entradas y opciones
2. **Configurar NPC** → Añadir scripts y asignar DialogueData
3. **Probar** → Caminar y presionar E
4. **Iterar** → Ajustar diálogo basado en las pruebas

### Consejos de Rendimiento:
- ✅ Mantener texto de opciones corto (que quepa en botones)
- ✅ Usar información de personaje por defecto (no repetir en cada DialogueData)
- ✅ Reutilizar assets DialogueData entre NPCs similares
- ✅ Usar nombres descriptivos para assets DialogueData

---