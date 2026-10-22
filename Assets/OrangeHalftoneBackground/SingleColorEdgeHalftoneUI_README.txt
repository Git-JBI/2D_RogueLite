Single Color Edge Halftone UI — Unity 6 / URP 17

Base Color로 배경색을 지정하면 위쪽 배경은 진하게, 아래쪽 배경은 연하게 자동 계산됩니다.
Top Dot Color / Bottom Dot Color로 양 끝 점의 색을 각각 직접 지정합니다.
점 색상은 Base Color와 독립적입니다.
Top 배경: Base Color에서 검정으로 14% 혼합.
Bottom 배경: Base Color에서 흰색으로 16% 혼합.
검정과 흰색 끝값에서는 더 어둡거나 더 밝아질 수 없으므로 대비가 줄어듭니다.

점 성장 범위(아래=0, 위=1)
- 0.25부터 아래 끝 0으로 갈수록 작은 원에서 큰 원으로 성장.
- 0.75부터 위 끝 1로 갈수록 작은 원에서 큰 원으로 성장.
- 가운데 0.25~0.75에는 점이 표시되지 않음.
행 중심 좌표로 크기를 계산하여 같은 행의 점 크기가 같고 원형을 유지합니다.

사용법
Canvas의 Raw Image > Material에 SingleColorEdgeHalftoneUI.mat를 지정합니다.
Raw Image의 Color는 흰색, UV Rect는 (0,0,1,1)로 설정합니다.
Height / Width는 UI 높이 / 너비, Dot Columns는 가로 점 개수입니다.
Maximum Dot Diameter는 끝에 있는 원의 최대 지름(셀 크기 대비), Dot Opacity는 점의 불투명도입니다.
기본 노드만 사용하며 Custom Function/HLSL/Sub Graph/텍스처는 사용하지 않습니다.
기존 OrangeHalftoneUI 버전과 씬은 수정하지 않았습니다.

색상 적용 수정: Disable Color Tint를 켜서 Image/Raw Image의 RGB가 머티리얼 색에 곱해지지 않습니다. UI의 알파는 Vertex Color → Split A → Alpha로 유지됩니다. 점 색상은 Material Inspector에서 설정하세요. Dot Opacity=1일 때 원의 내부가 지정 색상 그대로 표시되며 가장자리에는 안티앨리어싱이 적용됩니다.
