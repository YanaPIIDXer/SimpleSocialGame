# SimpleSocialGame
色々な実験で得られた知識を実践で試すための簡単なソーシャルゲーム

# 開発環境

| Key               | Value                 |
| ----------------- | --------------------- |
| クライアント      | Unity                 |
| サーバ            | Ruby on Rails(Docker) |
| インフラ          | AWS(ECS)              |
| インフラ管理      | Terraform             |
| CI/CDパイプライン | Github Actions        |

# 実装する機能

- 新規登録・ログイン
- ガチャ
  - 収集するだけ
- 石集め
  - ガチャを回すのに必要な通貨
  - 手順は以下の通り
    - ログボ
    - 課金

# 素材提供
[ジュエルセイバーFREE](http://www.jewel-s.jp/)

# 大雑把なインフラ構成図

![構成図](https://github.com/YanaPIIDXer/SimpleSocialGame/blob/develop/docs/Infrastructure.png)
