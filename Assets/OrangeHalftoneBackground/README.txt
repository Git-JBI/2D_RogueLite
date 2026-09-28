Orange Halftone UI — Unity 6000.0 / URP 17

기본 Shader Graph 노드만 사용하는 Canvas용 배경입니다.
Custom Function, 사용자 HLSL, Sub Graph, 이미지 텍스처를 사용하지 않습니다.

사용법
1. Canvas 아래 UI > Raw Image를 만들고 배경 크기로 늘립니다.
2. OrangeHalftoneUI.mat를 Raw Image의 Material에 지정합니다.
3. Color는 흰색, UV Rect는 (0,0,1,1)로 둡니다. Texture는 없어도 됩니다.
4. Height / Width 값을 표시 영역의 높이 / 너비로 설정하면 점이 원형을 유지합니다.
   기본값은 참고 이미지의 세로 비율(약 2.089)입니다.
5. 배경 UI의 Raycast Target을 끄면 다른 버튼 입력을 방해하지 않습니다.

조절 항목
Bottom Background / Orange Background: 아래쪽 및 중앙/위쪽 배경색
Bottom Glow Height: 아래쪽 노란 그라데이션 범위
Dot Columns: 가로 점 개수
Maximum Dot Diameter: 점의 최대 지름(셀 크기 대비)
Top/Bottom Band Position: 점무늬 띠의 중심 위치(0=아래, 1=위)
Top/Bottom Band Width: 띠의 반폭
Top/Bottom Dot Opacity, Dot Color: 점의 강도와 색

UI Image에도 사용할 수 있으나 아틀라스 Sprite UV는 반복 패턴에 영향을 줄 수 있어 Raw Image를 권장합니다.
색상과 주요 무늬를 절차적으로 재현한 버전이며 참고 PNG와 픽셀 단위로 같지는 않습니다.
기존 씬, 머티리얼, 그래프는 변경하지 않았습니다.
