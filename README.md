# Honoo Language Localisation Converter

- [Honoo Language Localisation Converter](#honoo-language-localisation-converter)
  - [INTRODUCTION](#introduction)
  - [SCREENSHOTS](#screenshots)
  - [EXAMPLES](#examples)
  - [CHANGELOG](#changelog)
    - [1.0.1](#101)
  - [LICENSE](#license)

## INTRODUCTION

Language file creator. Create xml file from template. And create C# code file to load Language file.

## SCREENSHOTS

![screenshots1](screenshots/screenshots1.png)

![screenshots2](screenshots/screenshots2.png)

## EXAMPLES

```xaml

<MenuItem Header="{Binding Menu.File, Source={x:Static vm:LanguagePackage.Instance}}">
    <MenuItem Command="{Binding CreateCommand}" Header="{Binding Menu.CreateTemplate, Source={x:Static vm:LanguagePackage.Instance}}" />
</MenuItem>

```

## CHANGELOG

### 1.0.1

**Features* Published.

## LICENSE

[Apache-2.0](LICENSE) license.
