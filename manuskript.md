# Præsentationsmanuskript: Space Shooter Spil

## 1. Introduktion (30 sekunder)
"Jeg vil gerne præsentere mit 2D space shooter spil, udviklet i Unity 6. Det er et top-down shoot'em-up spil, hvor spilleren styrer et rumskib og kæmper mod bølger af aliens."

## 2. Gameplay Mekanik (1-2 minutter)

### Spillerkontrol
"Spilleren styrer rumskibet med musen - skibet roterer automatisk mod musemarkøren. Når spilleren skyder, sker der noget unikt:"

**Kode reference:** `PlayerController.cs:92-116`
- Spilleren skyder projektiler mod musemarkøren (linje 92-99)
- Hver gang spilleren skyder, får de en **knockback-effekt** i den modsatte retning (linje 107-115)
- Dette skaber en interessant bevægelsesmekanik, hvor spilleren bruger skydning til både at angribe og bevæge sig

### Ammo System
"Spillet har et ammunition system:"
**Kode reference:** `PlayerController.cs:49-53`
- Max 15 skud (kan opgraderes via pickups)
- Regenererer 2 skud hver 2. sekund
- Spilleren skal tænke strategisk over, hvornår de skyder

## 3. Enemy AI (1 minut)

### Simpel men effektiv AI
"Fjendernes AI er designet til at være simpel, men effektiv:"

**Kode reference:** `EnemyAI.cs:47-56`
- Fjender forfølger direkte spilleren (linje 49)
- Men de har også **separation behavior** (linje 50-52) - de skubber fra hinanden for at undgå at klumpe sammen
- Dette skaber mere organiske og udfordrende angrebsmønstre

### Boss Mekanik
"Når spilleren når længere i spillet, møder de boss-fjender med komplekse angrebsmønstre:"

**Kode reference:** `BossAI.cs:95-109`
- Tre forskellige angrebsmønstre: Burst fire, spread shot, og minion spawning
- Bossen skifter mellem mønstre for variation (linje 83)
- Boss-statistikker skalerer med sværhedsgraden

## 4. Wave System (1 minut)

### Progressiv Sværhedsgrad
"Spillet bruger et bølge-system til at skalere sværhedsgraden:"

**Kode reference:** `EnemySpawner.cs:103-111`
- Wave 1 starter med 10 fjender
- Hver ny wave tilføjer 10 flere fjender (linje 106)
- Fjender spawner rundt om spilleren i en tilfældig afstand (linje 85-91)
- Fjendernes health og hastighed stiger med hver wave gennem `difficultyMultiplier` (linje 75-82)

## 5. Pickup & Progression System (1 minut)

### Tilfældige Upgrades
"Når fjender dør, kan de droppe pickup items med tilfældige upgrades:"

**Kode reference:** `Pickups.cs:25-67`
- **Health**: +20 max health (linje 34-39)
- **Damage**: +5 skade per skud (linje 42-47)
- **Ammo**: Refill ammo + 5 ekstra kapacitet (linje 50-56)
- **Knockback Power**: Stærkere knockback for hurtigere bevægelse (linje 59-64)

Dette skaber en roguelike-agtig progression, hvor hver run kan være forskellig.

## 6. Tekniske Detaljer (30 sekunder)

### Unity 6 Best Practices
"Projektet følger moderne Unity praksis:"
- Bruger Unity 6's nye `Rigidbody2D.linearVelocity` (ikke deprecated `velocity`) - se `PlayerController.cs:45, 82, 114`
- Event-driven arkitektur for pickups og enemy deaths (`Pickups.cs:14`, `GameManager.cs:31-32`)
- Component-baseret design med separation of concerns

## 7. Afslutning (30 sekunder)
"Spillet kombinerer simpel men tilfredsstillende combat-mekanik med progressiv sværhedsgrad og upgrades. Den unikke knockback-mekanik gør bevægelse til en integreret del af kampen, og AI'en skaber udfordrende men fair gameplay."

---

## Demonstrationstips
1. **Start med at vise basic movement og shooting** - fremhæv knockback-mekanikken
2. **Lad nogle fjender spawne** - vis hvordan de bevæger sig og separerer
3. **Saml en pickup** - vis upgrade-systemet i aktion
4. **Hvis muligt, vis en boss** - demonstrer de forskellige angrebsmønstre
5. **Afslut med at vise code snippets** fra key mechanics

## Vigtige Kode-Lokationer til Reference
- Player shooting + knockback: `PlayerController.cs:92-116`
- Enemy AI pursuit + separation: `EnemyAI.cs:47-76`
- Wave progression: `EnemySpawner.cs:103-111`
- Pickup system: `Pickups.cs:25-67`
- Boss attack patterns: `BossAI.cs:95-175`
