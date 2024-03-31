# 吾輩はメモ帳、整理はまだしない

* [ローカライズされた .NET IntelliSense ファイルのダウンロード](https://dotnet.microsoft.com/ja-jp/download/intellisense)
  * 原本となるXMLドキュメントはどこにあるのか
  * そもそもライセンスどうなってんだ
    * 多分これだと思うのです  
      https://github.com/dotnet/dotnet-api-docs/tree/main
* [.NET のローカライズされた IntelliSense ファイルをインストールする方法](https://learn.microsoft.com/ja-jp/dotnet/core/install/localized-intellisense)
* .NET 側のバージョンが大きく変わった場合にディレクトリを作成して直近版をコピペ
  * メジャーバージョンでの履歴はどうでもいい
* とりま VsLocalizedIntellisense.App が何世代か動くの確認して diff 考える

## ディレクトリ

* `raw-intellisense`
  * インテリセンスの原文
* `work-intellisense`
  * 編集対象
  * これをローカライズ版の原本とする

## ブランチ

* 原則 `master` に突っ込む
  * アプリ・リソース間の同時作業は行わない

## VsLocalizedIntellisense.App

* .NET Framework 4.8 で動かす
  * Windows 10 以上なら確実に動くでしょうという安易な考え
* NuGet/依存DLL は同梱しない
  * 単独 EXE 置き換えアップデートしたいので DLL がいるとしんどい
* 設定は app.config を使用する
* 一時ディレクトリ系は EXE ルートディレクトリを基点とする

## VsLocalizedIntellisense.Xml

* 各ローカライズXMLに原文を付与する
* 付与した XML をリポジトリに突っ込む
  * 手動前提
* 原文更新からローカライズファイルが差分検知できるようにすることが目的であるためすでに設定されている原文の上書きは行わない
* 名前空間付与で行けるだろうと思ったらだめだった悲しみすごい
* 新規で何かしない限り使うことはない
  * 原文側の何かが変わった時とか

## VsLocalizedIntellisense.Raw

* **諸々の事情でこわれてる**
* 以下は NuGet からドキュメント XML を取得する
  * NETStandard.Library.Ref
  * Microsoft.NETCore.App.Ref
  * Microsoft.WindowsDesktop.App.Ref
* 取得した XML を生データとしてリポジトリに突っ込む
  * 手動前提
* XML 壊れてるので Visual Studio 更新時の XML を信じた方がいいかも
  * ライセンスがほんと分からん
  * たぶんこのアプリを使うことはないと思われる

