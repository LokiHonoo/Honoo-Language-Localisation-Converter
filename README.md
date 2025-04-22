# Honoo Language Localisation Converter

- [Honoo Language Localisation Converter](#honoo-language-localisation-converter)
  - [INTRODUCTION](#introduction)
  - [SCREENSHOTS](#screenshots)
  - [EXAMPLES](#examples)
  - [LICENSE](#license)

## INTRODUCTION

Language file creator. Create xml file from template. And create C# code file to load Language file.

## SCREENSHOTS

![screenshot1](screenshots/screenshot1.png)

![screenshot2](screenshots/screenshot2.png)

## EXAMPLES

```xaml

<MenuItem Header="{Binding Menu.File, Source={x:Static vm:LanguagePackage.Instance}}">
    <MenuItem Command="{Binding CreateNewCommand}" Header="{Binding Menu.CreateNew, Source={x:Static vm:LanguagePackage.Instance}}" />
</MenuItem>

```

## LICENSE

[Apache-2.0](LICENSE) license.
