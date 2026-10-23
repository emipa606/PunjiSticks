# Punji Sticks (Continued) GitHub Copilot Instructions

## Mod Overview and Purpose
Punji Sticks (Continued) is an updated version of the Punji Sticks mod for RimWorld. This mod introduces a realistic spike trap called Punji Sticks, which promotes plague in its victims. The mod also includes a poison version of the trap and an anesthetic version that depends on the Anesthetic Gun MOD.

## Key Features and Systems
- **Punji Sticks Trap**: A realistic spike trap with severe bleeding effects and a 25% chance to spread or worsen the Plague effect.
- **Poison Version**: A variant of the Punji Sticks trap that inflicts poison damage.
- **Anesthetic Version**: A variant that uses the Anesthetic Gun MOD to inflict anesthetic damage.

## Coding Patterns and Conventions
- **Naming Conventions**: Use descriptive and consistent naming for classes, methods, and variables.
- **Code Structure**: Follow a clean and organized code structure, with well-defined methods and classes.
- **Comments**: Use comments to explain complex logic and ensure code readability.

## XML Integration
- **Defs**: Define new game entities such as ThingDef, RulePackDef, and DesignatorDropdownGroupDef in XML files.
- **Patches**: Use XML patches to modify existing game entities and behaviors.

## Harmony Patching
- **Harmony**: Utilize the Harmony library to patch existing game code.
- **Patching Strategy**: Patch game methods to add or modify behavior, such as adding disease spread or poison effects to the Punji Sticks trap.

## Suggestions for Copilot
- **Code Snippets**: Provide code snippets for common tasks, such as creating new traps, adding disease effects, and implementing poison damage.
- **XML Defs**: Offer examples of XML definitions for new game entities, including ThingDef and RulePackDef.
- **Harmony Patches**: Provide examples of Harmony patches for modifying existing game behavior, such as adding effects to existing traps.

## Example Code Snippets
csharp
// Example of creating a new trap
public class PunjiSticksTrap : Building_Trap
{
    public override void SpringSub(Thing hitThing)
    {
        // Code to handle the trap's effect
    }
}


xml
<!-- Example of a ThingDef definition -->
<ThingDef ParentName="Trap">
    <defName>PunjiSticks</defName>
    <label>Punji Sticks</label>
    <description>A realistic spike trap that causes severe bleeding and may spread plague.</description>
    <!-- Other properties -->
</ThingDef>


csharp
// Example of a Harmony patch
[HarmonyPatch(typeof(Trap), nameof(Trap.Spring))]
public static class PunjiSticks_Patch
{
    public static void Postfix(Trap __instance, Thing hitThing)
    {
        // Code to add disease spread or poison effects
    }
}


By following these guidelines and utilizing the code snippets provided, you can effectively contribute to the Punji Sticks (Continued) mod and enhance its functionality.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

