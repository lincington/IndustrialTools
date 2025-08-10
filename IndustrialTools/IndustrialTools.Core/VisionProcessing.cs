using HalconDotNet;


namespace IndustrialTools.Core
{
    public class VisionProcessing
    {
        public void ProcessImage()
        {
            HObject image;
            HOperatorSet.ReadImage(out image, "path_to_your_image.jpg");

            string folderpath = "";
            HOperatorSet.WriteImage(image, "jpg", 0, folderpath);
        }

       public static void ocrEngineTest()
        {
            var ocrEngine = new HalconDocumentOCR();

            try
            {
                // 1. 初始化OCR引擎
                ocrEngine.Initialize(); // 使用默认文档模型

                // 2. 识别文档
                string imagePath = "C:\\Users\\linci\\Desktop\\Screenshot 2025-08-09 230217.png";
                var result = ocrEngine.RecognizeDocument(imagePath);

                // 3. 输出结果
                if (result.HasError)
                {
                    Console.WriteLine($"识别错误: {result.ErrorMessage}");
                }
                else
                {
                    Console.WriteLine($"识别结果:\n{result.RecognizedText}");
                    Console.WriteLine($"平均置信度: {result.AverageConfidence:P}");

                    // 输出每行文本
                    Console.WriteLine("\n按行输出:");
                    foreach (var line in result.Lines)
                    {
                        Console.WriteLine($"行 {line.LineNumber}: {line.Text}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生异常: {ex.Message}");
            }
            finally
            {
                ocrEngine.Dispose();
            }
        }

    }

    public class HalconDocumentOCR
    {
        private HTuple _ocrHandle;
        private bool _isInitialized = false;

        /// <summary>
        /// 初始化OCR识别器
        /// </summary>
        /// <param name="ocrModelPath">OCR模型路径(默认使用Halcon内置文档模型)</param>
        public void Initialize(string ocrModelPath = "D:\\HALCON\\HALCON-25.05-Progress\\ocr\\Document_0-9A-Z_NoRej.omc")
        {
            try
            {
                // 释放已有资源
                if (_isInitialized)
                {
                    HOperatorSet.ClearOcrClassMlp(_ocrHandle);
                    _ocrHandle = null;
                }

                // 加载OCR模型
                HOperatorSet.ReadOcrClassMlp(ocrModelPath, out _ocrHandle);
                _isInitialized = true;

                Console.WriteLine($"OCR模型加载成功: {ocrModelPath}");
            }
            catch (HalconException ex)
            {
                _isInitialized = false;
                throw new Exception($"OCR初始化失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 执行文档OCR识别
        /// </summary>
        /// <param name="imagePath">图像路径</param>
        /// <returns>识别结果和置信度</returns>
        public DocumentOCRResult RecognizeDocument(string imagePath)
        {
            if (!_isInitialized)
            {
                throw new Exception("OCR识别器未初始化，请先调用Initialize方法");
            }

            HObject image = null;
            var result = new DocumentOCRResult();

            try
            {
                // 1. 读取图像
                HOperatorSet.ReadImage(out image, imagePath);


                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                result.ImageWidth = width.I;
                result.ImageHeight = height.I;

                // 2. 预处理图像
                var processedImage = PreprocessImage(image);

                // 3. 查找文本区域
                var textRegions = FindTextRegions(processedImage);

                // 4. 识别文本
                RecognizeText(textRegions, processedImage, ref result);

                return result;
            }
            catch (HalconException ex)
            {
                result.ErrorMessage = $"Halcon错误: {ex.Message}";
                return result;
            }
            finally
            {
                image?.Dispose();
            }
        }
        private bool _isModelLoaded = false;

        //// 执行OCR识别
        //public List<string> RecognizeText(HImage image)
        //{
        //    if (!_isModelLoaded)
        //    {
        //        throw new InvalidOperationException("OCR模型未初始化");
        //    }

        //    var results = new List<string>();

        //    try
        //    {
        //        // 1. 转换为灰度图像
        //        HImage grayImage = image.Rgb1ToGray();

        //        // 2. 图像增强
        //        HImage enhancedImage = grayImage.Emphasize(7, 7, 1.0);

        //        // 3. 二值化
        //        HImage binaryImage;
        //        HTuple usedThreshold;
        //        enhancedImage.BinaryThreshold("max_separability", "dark", out binaryImage, out usedThreshold);

        //        // 4. 查找文本区域
        //        HRegion connectedRegions = binaryImage.Connection();
        //        HRegion textRegions = connectedRegions.SelectShape(
        //            new HTuple("height", "width"),
        //            "and",
        //            new HTuple(10, 10),
        //            new HTuple(100, 100));

        //        // 5. 对每个区域进行OCR识别
        //        int regionCount = textRegions.CountObj();
        //        for (int i = 1; i <= regionCount; i++)
        //        {
        //            HRegion singleRegion = textRegions.SelectObj(i);
        //            HTuple classVal, confidence;

        //            _ocrHandle.DoOcrMultiClassMlp(singleRegion, grayImage, out classVal, out confidence);

        //            results.Add(classVal.S);
        //            Console.WriteLine($"识别结果: {classVal.S}, 置信度: {confidence.D}");
        //        }

        //        // 6. 显示结果（可选）
        //        if (regionCount > 0)
        //        {
        //            HWindow window = new HWindow(0, 0, 800, 600);
        //            window.SetColor("red");
        //            window.DispObj(image);
        //            window.SetDraw("margin");
        //            window.DispObj(textRegions);

        //            // 显示识别文本
        //            for (int i = 0; i < results.Count; i++)
        //            {
        //                HTuple row, column;
        //                singleRegion.AreaCenter(out row, out column);
        //                window.SetTposition(row.D + 10, column.D);
        //                window.WriteString(results[i]);
        //            }
        //        }

        //        return results;
        //    }
        //    catch (HalconException ex)
        //    {
        //        Console.WriteLine($"OCR识别过程中出错: {ex.Message}");
        //        return results;
        //    }
        //}

        // 释放资源
        //public void Dispose()
        //{
        //    if (_isModelLoaded)
        //    {
        //        _ocrHandle.Dispose();
        //        _isModelLoaded = false;
        //    }
        //}



        private HObject PreprocessImage(HObject image)
        {
            HObject grayImage, enhancedImage;

            // 转换为灰度图
            HOperatorSet.Rgb1ToGray(image, out grayImage);

            // 增强对比度（适用于低对比度文档）
            HOperatorSet.Emphasize(grayImage, out enhancedImage, 7, 7, 1.5);

            // 可选：去噪
            // HOperatorSet.MedianImage(enhancedImage, out enhancedImage, "circle", 1, "mirrored");

            return enhancedImage;
        }

        private HObject FindTextRegions(HObject image)
        {
            HObject regions, connectedRegions, selectedRegions;

            // 使用局部阈值分割文本
            HOperatorSet.VarThreshold(image, out regions, 15, 15, 0.2, 2, "dark");

            // 连接区域
            HOperatorSet.Connection(regions, out connectedRegions);

            // 选择符合文本特征的区域
            HOperatorSet.SelectShape(connectedRegions, out selectedRegions,
                new HTuple("area", "width", "height", "rectangularity"),
                "and",
                new HTuple(50, 5, 5, 0.3),
                new HTuple(99999, 500, 500, 1.0));

            // 形态学处理（合并相邻文本区域）
            HOperatorSet.UnionAdjacentContoursXld(selectedRegions, out HObject mergedRegions,
                10, 1, "attr_keep");

            return mergedRegions;
        }

        private void RecognizeText(HObject textRegions, HObject image, ref DocumentOCRResult result)
        {
            // 按行和列排序区域
            HOperatorSet.SortRegion(textRegions, out HObject sortedRegions,
                "upper_left", "true", "column");

            // 执行OCR识别
            HOperatorSet.DoOcrMultiClassMlp(sortedRegions, image, _ocrHandle,
                out HTuple characters, out HTuple confidence);

            // 处理识别结果
            result.RecognizedText = characters.S;
            result.AverageConfidence = confidence.TupleMean().D;

            // 获取每个字符的详细信息
            result.Characters = new List<OCRCharacter>();
            for (int i = 0; i < characters.Length; i++)
            {
                result.Characters.Add(new OCRCharacter
                {
                    Text = characters[i].S,
                    Confidence = confidence[i].D,
                    Position = i + 1
                });
            }

            // 如果需要获取字符位置（需要额外计算）
            GetCharacterPositions(sortedRegions, ref result);
        }

        private void GetCharacterPositions(HObject regions, ref DocumentOCRResult result)
        {
            // 获取每个区域的坐标信息
            HOperatorSet.AreaCenter(regions, out HTuple areas, out HTuple rows, out HTuple cols);
            HOperatorSet.SmallestRectangle1(regions, out HTuple row1, out HTuple col1,
                out HTuple row2, out HTuple col2);

            for (int i = 0; i < result.Characters.Count; i++)
            {
                if (i < row1.Length)
                {
                    result.Characters[i].BoundingBox = new Rectangle
                    {
                        X = col1[i].I,
                        Y = row1[i].I,
                        Width = col2[i].I - col1[i].I,
                        Height = row2[i].I - row1[i].I
                    };
                }
            }

            // 分组为行（基于Y坐标）
            result.Lines = result.Characters
                .GroupBy(c => (int)(c.BoundingBox.Y / 20)) // 20是行高阈值，根据需要调整
                .OrderBy(g => g.Key)
                .Select(g => new OCRLine
                {
                    LineNumber = g.Key + 1,
                    Text = string.Join("", g.OrderBy(c => c.BoundingBox.X).Select(c => c.Text)),
                    Characters = g.OrderBy(c => c.BoundingBox.X).ToList()
                })
                .ToList();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (_isInitialized)
            {
                HOperatorSet.ClearOcrClassMlp(_ocrHandle);
                _isInitialized = false;
            }
        }
    }

    /// <summary>
    /// OCR识别结果
    /// </summary>
    public class DocumentOCRResult
    {
        public string RecognizedText { get; set; }
        public double AverageConfidence { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public List<OCRCharacter> Characters { get; set; }
        public List<OCRLine> Lines { get; set; }
        public string ErrorMessage { get; set; }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    }

    public class OCRCharacter
    {
        public string Text { get; set; }
        public double Confidence { get; set; }
        public int Position { get; set; }
        public Rectangle BoundingBox { get; set; }
    }

    public class OCRLine
    {
        public int LineNumber { get; set; }
        public string Text { get; set; }
        public List<OCRCharacter> Characters { get; set; }
    }

    public class Rectangle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }



}
