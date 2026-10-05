# Metilest 2 (legacy)

**Recover the composition of a fat/oil blend from its gas chromatography of fatty acid methyl esters.**

A Windows-only C# / WinForms application targeting .NET Framework 3.5. Original
release 2009, copyright 2009–2015 Alessandro Stanga. Licensed under the
[GNU General Public License v3](LICENSE).

---

## What it does

You have a gas chromatography report for a fat or oil sample — the percentage of
each **fatty acid**, obtained by GC of the fatty acid *methyl esters*. You suspect
the sample is a blend of several known fats (palm oil, coconut, sunflower, butter,
lard, …) and you want to know **which fats it contains, and in what proportion**.

Metilest answers that question. It keeps a library of reference fatty-acid
profiles for 32 candidate slots — 29 named fats plus three free ones — simulates
blends, computes the *theoretical* chromatography of each blend and compares it
against the one you measured, keeping the combination that matches best.

Put simply, from the manual:

> *Calcola rapidamente la qualità e la quantità degli oli e grassi componenti una
> miscela, avendo a disposizione la gascromatografia degli esteri metilici.*

> Rapidly calculates the quality and quantity of the oils and fats making up a
> blend, given the gas chromatography of the methyl esters.

This is a **reverse problem**: many different blends can produce very similar
chromatograms. The program relies on you to narrow the search — which is why the
manual insists that *some* knowledge of fat composition matters. The fewer fats
you enable, the faster and more decisive the result.

## How it works

1. **Enter the chromatography** — the measured percentage of each of the 24 fatty
   acids (C4:0 → C22:1). The input mask pre-fills the decimal separator.
2. **Set search ranges** — for every candidate fat, a `Min` / `Max` percentage.
   The ranges should be generous enough that their maxima sum to more than 100;
   the search only considers combinations totalling exactly 100.
3. **Choose a step** — the granularity of the simulated blending, in percent.
   Start coarse when many fats or wide ranges are enabled, then tighten it.
4. **Run** — the progress window shows the combination currently under test in
   *orange* and the best result so far in *green*, refreshed every 10% so as not
   to waste time repainting. Stop and resume at will.
5. **Read the result** — optimal blend percentages, the resulting fatty-acid
   profile, the approximation error, and the **iodine number**.

### The search

Candidate blends are enumerated across the ranges you supplied, keeping the one
with the smallest total deviation between measured and theoretical profiles
(sum of absolute differences over all 24 fatty acids).

### The iodine number

Computed from the unsaturated fatty acids of the winning blend:

```
NI = (C16:1·99.78 + C17:1·94.9 + C18:1ⁱˢᵒ·89.9 + C18:1·89.9
    + C18:2·181.04 + C18:3·273.52 + C20:1·81.75 + C22:1·74.98) / 100
NI = NI − 5%            // empirical correction factor
```

The value is shown on the results screen and written into exported reports.

## The standards library

Reference profiles live in `default.ini` — one line per fat, `;`-separated,
comma as the decimal separator:

```
PalmaRaff;0;0;0;0;0,3;1,2;0;0,04;0;44,3;0,2;0;0,1;0;1;4,4;0,1;38,76;9,9;...
└─ fat key ┴──────────── 24 fatty acid percentages ─────────────────────┘
```

Values are bibliographic averages, not measured constants — treat them as
approximate and edit them for your own laboratory. Open
**Visualizza → Tabella degli standard** (*View → Standards table*) to inspect and
change them, then **File → Salva**.

Candidate fats include palm (raffined, African, oleins IV60/62/64, stearins
48/53), coconut (raffined, hydrogenated), palm kernel (four fractions), soy,
rapeseed, peanut, grape seed, sunflower (high-linoleic and high-oleic), sesame,
hazelnut, olive, cocoa butter, babassu, shea, butter, lard and tallow — plus
three free slots (`X`, `Y`, `Z`) for your own.

## Exporting results

Finished reports export to **ODT** (OpenOffice), **DOC** (Word) and **PDF** by
filling bookmarks in a template document. This requires **OpenOffice 3.0 or
later** installed — the export drives OpenOffice through its UNO API.

## Building

Visual Studio (2017 or later) on Windows:

```
metilest2009.sln
```

- .NET Framework 3.5, `x86`, output type `WinExe`
- `metilest2009/metilest2.csproj` is the project the solution builds
  (`metilest2009.csproj` is an alternate copy with a different assembly name)

`Form1.cs` and `Form2.cs` are present but referenced by neither project — they
are unreferenced leftovers, and nothing in the build depends on them.

## Repository layout

```
metilest 2009/
├── metilest2009.sln           solution
├── LICENSE                    GNU GPL v3
├── .gitignore                 build output, VS state, backup snapshots
└── metilest2009/
    ├── Program.cs             entry point → InsermentoDati (data entry form)
    ├── InsDati.*              chromatography input + search parameters
    ├── Calcolo.*              the search, progress display, iodine number
    ├── Report.*               results screen and document export
    ├── Standard.*             standards table (view/edit reference profiles)
    ├── AboutBoxMet.*          about box
    ├── Form1.*, Form2.*       unreferenced leftovers (see above)
    ├── app.config
    ├── metilest2.csproj       the project built by the solution
    ├── metilest2009.csproj    alternate project file
    └── Properties/
```

Companion material — the user manual (`manuale/`), reference spreadsheets and
sample reports (`documenti/`), the shipped binary and report template
(`binario/`), and the OpenOffice UNO assemblies (`dllopenoffice/`) — lives in
the parent `Metilest2` repository, not here.

## History

- **1991** — the idea in BASIC, *"by Fabri & Sergio, giugno 1991"*. A scan of
  the original listing and printout survives in the parent repository
  (`documenti/tabulato metilest1.*`), complete with `PLAY` statements and a
  `LOCATE`-based screen.
- **2009** — rewritten for Windows as Metilest 2 (this repository).
- **2015** — published to GitHub.
- **2026** — the licensing model was **removed entirely** (no activation, no
  licence dialog), the solution was upgraded from VS2008 to VS2017, and the
  project directory was restored to the depth its `.sln` expects.

## The modern port

A cross-platform rewrite in .NET 8 + Avalonia lives in the parent `Metilest2`
repository (`Metilest2.Avalonia/`). It reproduces the same two-step workflow and
optimiser, runs on Windows, Linux and macOS, and adds branch-and-bound pruning
and multi-core parallelism to the search.

It does **not** yet reproduce report export or the standards-table editor —
those remain exclusive to this Windows build.

## License

GNU General Public License v3.0 or later — see [LICENSE](LICENSE).
