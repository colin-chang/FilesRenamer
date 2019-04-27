# Renamer
批量重命名文件(Windows/mac OS/Linux)工具。

## 功能
批量修改给定目录下(可递归搜索)文件名，按照特定规则替换文件名内容，如**去除文件名前缀或者后缀**

## 技术
工具基于.Net Core 2.2开发。系统需要安装.Net Core Runtime才可以运行该程序

## 使用
1. [下载](https://github.com/colin-chang/renamer/releases/download/1.0/rename-release.zip)软件并解压,在解压目录下通过命令行执行程序

    ```sh
    # 递归将 ～/Desktop 下所有文件名中的"test"替换为"_"
    $ dotnet ColinChang.Renamer.dll ~/Desktop -y "test" "_"
    ```

2. Docker

```sh
# clone code
$ git clone git@github.com:colin-chang/FilesRenamer.git && cd FilesRenamer

# build image 
$ docker build -t colinchang/renamer .

# 递归将 ～/Desktop 下所有文件名中的"test"替换为"_"
$ docker run -it --rm \
-v ~/Desktop:/app/root \
-e REPLACE_FROM="test" \
-e REPLACE_TO="_" \
-e RECURSION="-y" \
colinchang/renamer
```